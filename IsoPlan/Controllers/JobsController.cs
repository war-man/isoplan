using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IsoPlan.Data.DTOs;
using IsoPlan.Data.Entities;
using IsoPlan.Exceptions;
using IsoPlan.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IsoPlan.Controllers
{    
    [ApiController]
    [Route("api/[controller]")]
    public class JobsController : ControllerBase
    {
        private readonly IJobService _jobService;
        private readonly IMapper _mapper;

        public JobsController(
            IJobService jobService,
            IMapper mapper)
        {
            _jobService = jobService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<JobDTO> jobDtos = _mapper.Map<List<JobDTO>>(_jobService.GetAll());
            return Ok(jobDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Job job = _jobService.GetById(id);

            if (job == null)
            {
                return NotFound("Job not found");
            }

            var jobDto = _mapper.Map<JobDTO>(job);
            return Ok(jobDto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] JobDTO jobDto)
        {
            var job = _mapper.Map<Job>(jobDto);
            _jobService.Create(job);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] JobDTO jobDto)
        {
            var job = _mapper.Map<Job>(jobDto);
            job.Id = id;
            
            // save 
            _jobService.Update(job);
            return NoContent();          
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {            
            _jobService.Delete(id);
            return NoContent();            
        }

        [HttpPost("item")]
        public IActionResult CreateJobItem([FromBody] JobItemDTO jobItemDTO)
        {
            var jobItem = _mapper.Map<JobItem>(jobItemDTO);
            _jobService.CreateJobItem(jobItem);
            return StatusCode(201);
        }

        [HttpPut("item/{id}")]
        public IActionResult UpdateJobItem(int id, [FromBody] JobItemDTO jobItemDTO)
        {
            var jobItem = _mapper.Map<JobItem>(jobItemDTO);
            jobItem.Id = id;

            // save 
            _jobService.UpdateJobItem(jobItem);
            return NoContent();
        }

        [HttpDelete("item/{id}")]
        public IActionResult DeleteJobItem(int id)
        {
            _jobService.DeleteJobItem(id);
            return NoContent();
        }
    }
}