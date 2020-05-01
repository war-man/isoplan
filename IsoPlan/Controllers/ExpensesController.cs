using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IsoPlan.Data.DTOs;
using IsoPlan.Data.Entities;
using IsoPlan.Exceptions;
using IsoPlan.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace IsoPlan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, Manager")]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpenseService _expenseService;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly ICustomAuthService _customAuthService;

        public ExpensesController(
            IExpenseService expenseService,
            IMapper mapper,
            IFileService fileService,
            ICustomAuthService customAuthService)
        {
            _expenseService = expenseService;
            _mapper = mapper;
            _fileService = fileService;
            _customAuthService = customAuthService;
        }

        [HttpGet]
        public IActionResult Get(int jobId, string startDate, string endDate)
        {
            IEnumerable<ExpenseDTO> expenseDTOs = _mapper.Map<List<ExpenseDTO>>(_expenseService.GetAll(jobId, startDate, endDate));
            return Ok(expenseDTOs);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Expense expense = _expenseService.GetById(id);

            if (expense == null)
            {
                return NotFound("Expense not found");
            }

            var expenseDTO = _mapper.Map<ExpenseDTO>(expense);
            return Ok(expenseDTO);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ExpenseDTO expenseDTO)
        {
            Expense expense = _mapper.Map<Expense>(expenseDTO);
            _expenseService.Create(expense);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ExpenseDTO expenseDTO)
        {
            Expense expense = _mapper.Map<Expense>(expenseDTO);
            expense.Id = id;

            // save 
            _expenseService.Update(expense);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Expense expense = _expenseService.GetById(id);
            if (expense == null)
            {
                throw new AppException("Expense not found");
            }

            _fileService.DeleteDirectory(Path.Combine("Jobs", expense.JobId.ToString(), "internalExpenses", id.ToString()));
            _expenseService.Delete(id);
            return NoContent();
        }

        [HttpPost("Files")]
        public ActionResult UploadExpenseFile([FromForm]ExpenseFileDTO expenseFileDTO)
        {
            Expense expense = _expenseService.GetById(expenseFileDTO.Id);

            if (expense == null)
            {
                throw new AppException("Expense not found");
            }

            if (!string.IsNullOrEmpty(expense.FilePath))
            {
                throw new AppException("Please delete previous expense");
            }

            string path = Path.Combine("Jobs", expense.JobId.ToString(), "internalExpenses", expenseFileDTO.Id.ToString());

            _fileService.Create(expenseFileDTO.File, path);

            expense.FilePath = Path.Combine(path, expenseFileDTO.File.FileName);

            _expenseService.Update(expense);

            return Ok();
        }

        [HttpDelete("Files/{id}")]
        public IActionResult DeleteExpenseFile(int id)
        {
            var expense = _expenseService.GetById(id);
            if (expense == null)
            {
                throw new AppException("Expense not found");
            }
            _fileService.Delete(expense.FilePath);
            expense.FilePath = "";
            _expenseService.Update(expense);
            return NoContent();
        }

        [HttpGet("Files/{id}")]
        [AllowAnonymous]
        async public Task<ActionResult> DownloadFactureFile(int id, string token)
        {
            if (!_customAuthService.CheckToken(token))
            {
                return Unauthorized();
            }

            Expense expense = _expenseService.GetById(id);

            if (expense == null)
            {
                throw new AppException("Expense not found");
            }

            string fileName = Path.GetFileName(expense.FilePath);
            string filePath = expense.FilePath;

            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(fileName, out contentType))
            {
                contentType = "application/octet-stream";
            }

            string fullPath = _fileService.getFullPath(filePath);

            var memory = new MemoryStream();
            using (var stream = new FileStream(fullPath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, contentType);
        }
    }
}