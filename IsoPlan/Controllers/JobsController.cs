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
                return NotFound("Construction site not found");
            }

            var jobDto = _mapper.Map<JobDTO>(job);
            return Ok(jobDto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] JobDTO jobDto)
        {
            var constructionSite = _mapper.Map<Job>(jobDto);
            _jobService.Create(constructionSite);
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
    }
}