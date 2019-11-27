﻿using System;
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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace IsoPlan.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public EmployeesController(
            IEmployeeService employeeService,
            IMapper mapper,
            IWebHostEnvironment env)
        {
            _employeeService = employeeService;
            _mapper = mapper;
            _env = env;

        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<EmployeeDTO> employeeDtos = _mapper.Map<List<EmployeeDTO>>(_employeeService.GetAll());
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

        [HttpPost("files")]
        public ActionResult UploadEmployeeFile([FromForm]EmployeeFileDTO employeeFileDTO)
        {
            Employee employee = _employeeService.GetById(employeeFileDTO.EmployeeId);

            if (employee == null)
            {
                throw new AppException("Employee not found");
            }

            string guid = System.Guid.NewGuid().ToString();

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

        [HttpGet("files/{id}")]
        async public Task<ActionResult> DownloadEmployeeFile(int id)
        {
            EmployeeFile file = _employeeService.GetFile(id);

            if(file == null)
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

            return File(memory, contentType, fileName);          
        }

        [HttpDelete("files/{id}")]
        public IActionResult DeleteEmployeeFile(int id)
        {
            _employeeService.DeleteFile(id);
            return NoContent();
        }

        [HttpGet("{employeeId}/files")]
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