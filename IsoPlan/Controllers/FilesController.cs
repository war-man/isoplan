using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IsoPlan.Data.DTOs;
using IsoPlan.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace IsoPlan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public FilesController(IWebHostEnvironment env) {
            _env = env;
        }

        [HttpPost]
        public ActionResult Upload([FromForm]FileDTO fileDto)
        {
            string guid = System.Guid.NewGuid().ToString();

            var file = fileDto.File;

            string contentRootPath = _env.ContentRootPath;
            string webRootPath = _env.WebRootPath;

            if (file.Length > 0)
            {
                using (var fileStream = new FileStream(Path.Combine(webRootPath, "..\\Files\\" +  guid + "_" + file.FileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }

            return Ok(new { status = true, message = "File Posted Successfully" });
        }
    }
}