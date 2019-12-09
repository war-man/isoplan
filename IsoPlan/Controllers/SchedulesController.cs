using AutoMapper;
using IsoPlan.Data.DTOs;
using IsoPlan.Data.Entities;
using IsoPlan.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace IsoPlan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulesController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;
        private readonly IMapper _mapper;
        public SchedulesController(
            IScheduleService scheduleService,
            IMapper mapper)
        {
            _scheduleService = scheduleService;
            _mapper = mapper;
        }


        [HttpGet("total")]
        public IActionResult GetTotalPerEmployee(string date)
        {
            return Ok(_scheduleService.GetTotalPerEmployee(DateTime.Parse(date)));
        }

        [HttpGet("employee/{id}")]
        public IActionResult GetJobsPerEmployee(int id, string date)
        {
            return Ok(_scheduleService.GetJobsPerEmployee(id, DateTime.Parse(date)));
        }

        [HttpGet("job/{id}")]
        public IActionResult GetEmployeesPerJob(int id, string date)
        {
            return Ok(_scheduleService.GetEmployeesPerJob(id, DateTime.Parse(date)));
        }

        [HttpGet]
        public IActionResult GetWeek(string date)
        {
            List<ScheduleWeekDTO> weeks = _mapper.Map<List<ScheduleWeekDTO>>(_scheduleService.GetWeeks(DateTime.Parse(date)));
            return Ok(weeks);
        }

        [HttpPost]
        public IActionResult Add(ScheduleAddDTO scheduleAddDTO)
        {
            Job job = _mapper.Map<Job>(scheduleAddDTO.Job);
            List<Employee> employees = _mapper.Map<List<Employee>>(scheduleAddDTO.Employees);
            _scheduleService.Add(scheduleAddDTO.Date, job, employees);
            return StatusCode(201);
        }

        [HttpPut]
        public IActionResult Update(ScheduleDTO scheduleDTO)
        {
            Schedule schedule = _mapper.Map<Schedule>(scheduleDTO);

            _scheduleService.Update(schedule);

            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(ScheduleDTO scheduleDTO)
        {
            Schedule schedule = _mapper.Map<Schedule>(scheduleDTO);

            _scheduleService.Delete(schedule);

            return NoContent();
        }
    }
}