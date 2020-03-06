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

    public class FacturesController : ControllerBase
    {
        private readonly IFactureService _factureService;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly ICustomAuthService _customAuthService;

        public FacturesController(
            IFactureService factureService, 
            IMapper mapper, 
            IFileService fileService,
            ICustomAuthService customAuthService)
        {
            _factureService = factureService;
            _mapper = mapper;
            _fileService = fileService;
            _customAuthService = customAuthService;
        }

        [HttpGet]
        public IActionResult Get(int jobId, string startDate, string endDate)
        {
            IEnumerable<FactureDTO> jobDtos = _mapper.Map<List<FactureDTO>>(_factureService.GetAll(jobId, startDate, endDate));
            return Ok(jobDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Facture facture = _factureService.GetById(id);

            if (facture == null)
            {
                return NotFound("Facture not found");
            }

            var factureDTO = _mapper.Map<FactureDTO>(facture);
            return Ok(factureDTO);
        }

        [HttpPost]
        public IActionResult Create([FromBody] FactureDTO factureDTO)
        {
            Facture facture = _mapper.Map<Facture>(factureDTO);
            _factureService.Create(facture);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] FactureDTO factureDTO)
        {
            Facture facture = _mapper.Map<Facture>(factureDTO);
            facture.Id = id;

            // save 
            _factureService.Update(facture);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _fileService.DeleteDirectory(Path.Combine("Factures", id.ToString()));
            _factureService.Delete(id);
            return NoContent();
        }

        [HttpPost("Files")]
        public ActionResult UploadFactureFile([FromForm]FactureFileDTO factureFileDTO)
        {
            Facture facture = _factureService.GetById(factureFileDTO.Id);

            if(facture == null)
            {
                throw new AppException("Facture not found");
            }

            if (!string.IsNullOrEmpty(facture.FilePath))
            {
                throw new AppException("Please delete previous facture");
            }

            string path = Path.Combine("Jobs", facture.JobId.ToString(),"internalFactures", factureFileDTO.Id.ToString());

            _fileService.Create(factureFileDTO.File, path);

            facture.FilePath = Path.Combine(path, factureFileDTO.File.FileName);

            _factureService.Update(facture);

            return Ok();
        }

        [HttpDelete("Files/{id}")]
        public IActionResult DeleteFactureFile(int id)
        {
            var facture = _factureService.GetById(id);
            if (facture == null)
            {
                throw new AppException("Facture not found");
            }
            _fileService.DeleteDirectory(facture.FilePath);
            facture.FilePath = "";
            _factureService.Update(facture);
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

            Facture facture = _factureService.GetById(id);

            if (facture == null)
            {
                throw new AppException("Facture not found");
            }

            string fileName = Path.GetFileName(facture.FilePath);
            string filePath = facture.FilePath;

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