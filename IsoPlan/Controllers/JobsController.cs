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
    public class JobsController : ControllerBase
    {
        private readonly IJobService _jobService;
        private readonly ICustomAuthService _customAuthService;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public JobsController(
            IJobService jobService,
            IMapper mapper,
            IFileService fileService,
            ICustomAuthService customAuthService)
        {
            _jobService = jobService;
            _mapper = mapper;
            _fileService = fileService;
            _customAuthService = customAuthService;
        }

        [HttpGet]
        public IActionResult Get(string status, string startDate, string endDate)
        {
            IEnumerable<JobDTO> jobDtos = _mapper.Map<List<JobDTO>>(_jobService.GetAll(status, startDate, endDate));
            return Ok(jobDtos);
        }

        [HttpGet("bySchedules")]
        public IActionResult GetbySchedules(string startDate)
        {
            IEnumerable<JobDTO> jobDtos =
                _mapper.Map<List<JobDTO>>(_jobService.GetbySchedules(DateTime.Parse(startDate)));
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
            _fileService.DeleteDirectory(Path.Combine("Jobs", id.ToString()));
            return NoContent();
        }

        [HttpPost("Items")]
        public IActionResult CreateJobItem([FromBody] JobItemDTO jobItemDTO)
        {
            var jobItem = _mapper.Map<JobItem>(jobItemDTO);
            _jobService.CreateJobItem(jobItem);
            return StatusCode(201);
        }

        [HttpPut("Items/{id}")]
        public IActionResult UpdateJobItem(int id, [FromBody] JobItemDTO jobItemDTO)
        {
            var jobItem = _mapper.Map<JobItem>(jobItemDTO);
            jobItem.Id = id;

            // save 
            _jobService.UpdateJobItem(jobItem);
            return NoContent();
        }

        [HttpDelete("Items/{id}")]
        public IActionResult DeleteJobItem(int id)
        {
            _jobService.DeleteJobItem(id);
            return NoContent();
        }

        [HttpPost("Files")]
        public ActionResult UploadJobFile([FromForm]JobFileDTO jobFileDTO)
        {
            Job job = _jobService.GetById(jobFileDTO.JobId);

            if (job == null)
            {
                throw new AppException("Job not found");
            }

            if (!JobFolder.JobFolderList.Contains(jobFileDTO.Folder))
            {
                throw new AppException("Folder not found in predefined folders.");
            }

            var files = jobFileDTO.Files;

            string path = Path.Combine("Jobs", job.Id.ToString(), jobFileDTO.Folder);

            foreach (var file in files)
            {
                _fileService.Create(file, path);

                JobFile jobFile = new JobFile
                {
                    Name = file.FileName,
                    Path = Path.Combine(path, file.FileName),
                    JobId = job.Id,
                    Job = job,
                    Folder = jobFileDTO.Folder
                };

                _jobService.CreateFile(jobFile);
            }            

            return Ok();
        }

        [HttpGet("Files/{id}")]
        [AllowAnonymous]
        async public Task<ActionResult> DownloadJobFile(int id, string token)
        {
            if (!_customAuthService.CheckToken(token))
            {
                return Unauthorized();
            }

            JobFile file = _jobService.GetFile(id);

            if (file == null)
            {
                throw new AppException("File not found in database.");
            }

            string fileName = file.Name;

            string filePath = file.Path;

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

        [HttpDelete("Files/{id}")]
        public IActionResult DeleteJobFile(int id)
        {
            var file = _jobService.GetFile(id);
            if (file == null)
            {
                throw new AppException("File not found in database");
            }
            _fileService.Delete(file.Path);
            _jobService.DeleteFile(id);
            return NoContent();
        }

        [HttpGet("{jobId}/Files")]
        public IActionResult GetJobFiles(int jobId)
        {
            return Ok(_jobService.GetFiles(jobId));
        }
    }
}