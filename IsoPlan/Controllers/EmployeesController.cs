using System;
using System.Collections.Generic;
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

namespace IsoPlan.Controllers
{    
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeesController(
            IEmployeeService employeeService,
            IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
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
    }
}