using AutoMapper;
using IsoPlan.Data.DTOs;
using IsoPlan.Data.Entities;
using IsoPlan.Exceptions;
using IsoPlan.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace IsoPlan.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin, Manager")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ICustomAuthService _customAuthService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public EmployeesController(
            IEmployeeService employeeService,
            IMapper mapper,
            IWebHostEnvironment env,
            ICustomAuthService customAuthService)
        {
            _employeeService = employeeService;
            _mapper = mapper;
            _env = env;
            _customAuthService = customAuthService;
        }

        [HttpGet]
        public IActionResult Get(string status)
        {
            IEnumerable<EmployeeDTO> employeeDtos = _mapper.Map<List<EmployeeDTO>>(_employeeService.GetAll(status));
            return Ok(employeeDtos);
        }

        [HttpGet("bySchedules")]
        public IActionResult GetbySchedules(string startDate)
        {
            IEnumerable<EmployeeDTO> employeeDtos =
                _mapper.Map<List<EmployeeDTO>>(_employeeService.GetbySchedules(DateTime.Parse(startDate)));
            return Ok(employeeDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Employee employee = _employeeService.GetById(id);

            if (employee == null)
            {
                return NotFound("Employee not found");
            }

            var employeeDto = _mapper.Map<EmployeeDTO>(employee);
            return Ok(employeeDto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] EmployeeDTO employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            _employeeService.Create(employee);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] EmployeeDTO employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            employee.Id = id;

            // save 
            _employeeService.Update(employee);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _employeeService.Delete(id);
            return NoContent();
        }

        [HttpPost("Files")]
        public ActionResult UploadEmployeeFile([FromForm]EmployeeFileDTO employeeFileDTO)
        {
            Employee employee = _employeeService.GetById(employeeFileDTO.EmployeeId);

            if (employee == null)
            {
                throw new AppException("Employee not found");
            }

            string guid = Guid.NewGuid().ToString();

            var file = employeeFileDTO.File;

            string root = _env.WebRootPath;

            string path = Path.Combine(guid + "_" + file.FileName);

            string fullPath = Path.Combine(root, "..\\Files\\" + path);

            if (file.Length > 0)
            {
                EmployeeFile employeeFile = new EmployeeFile
                {
                    Name = employeeFileDTO.File.FileName,
                    Path = path,
                    EmployeeId = employee.Id,
                    Employee = employee
                };
                _employeeService.CreateFile(employeeFile);
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

            }

            return Ok();
        }

        [HttpGet("Files/{id}")]
        [AllowAnonymous]
        async public Task<ActionResult> DownloadEmployeeFile(int id, string token)
        {
            if (!_customAuthService.CheckToken(token))
            {
                return Unauthorized();
            }

            EmployeeFile file = _employeeService.GetFile(id);

            if (file == null)
            {
                throw new AppException("File not found in database.");
            }

            string fileName = file.Name;

            string filePath = file.Path;

            string webRootPath = _env.WebRootPath;

            string fullPath = Path.Combine(webRootPath, "..\\Files\\" + filePath);

            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(fileName, out contentType))
            {
                contentType = "application/octet-stream";
            }

            var memory = new MemoryStream();
            using (var stream = new FileStream(fullPath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, contentType);
        }

        [HttpDelete("Files/{id}")]
        public IActionResult DeleteEmployeeFile(int id)
        {
            _employeeService.DeleteFile(id);
            return NoContent();
        }

        [HttpGet("{employeeId}/Files")]
        public IActionResult GetEmployeeFiles(int employeeId)
        {
            return Ok(new[]
            {
                new
                {
                    Header="Fichiers",
                    Items=_employeeService.GetFiles(employeeId)
                }
            });
        }
    }
}