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
        private readonly IJobService _constructionSiteService;
        private readonly IMapper _mapper;

        public JobsController(
            IJobService constructionSiteService,
            IMapper mapper)
        {
            _constructionSiteService = constructionSiteService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<JobDTO> constructionSiteDtos = _mapper.Map<List<JobDTO>>(_constructionSiteService.GetAll());
            return Ok(constructionSiteDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Job constructionSite = _constructionSiteService.GetById(id);

            if (constructionSite == null)
            {
                return NotFound("Construction site not found");
            }

            var constructionSiteDto = _mapper.Map<JobDTO>(constructionSite);
            return Ok(constructionSiteDto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] JobDTO constructionSiteDto)
        {
            var constructionSite = _mapper.Map<Job>(constructionSiteDto);
            _constructionSiteService.Create(constructionSite);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] JobDTO constructionSiteDto)
        {
            var constructionSite = _mapper.Map<Job>(constructionSiteDto);
            constructionSite.Id = id;
            
            // save 
            _constructionSiteService.Update(constructionSite);
            return NoContent();          
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {            
            _constructionSiteService.Delete(id);
            return NoContent();            
        }
    }
}