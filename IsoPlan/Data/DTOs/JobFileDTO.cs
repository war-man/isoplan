using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsoPlan.Data.DTOs
{
    public class JobFileDTO
    {
        public IFormFile File { get; set; }
        public int JobId { get; set; }
        public string Folder { get; set; }
    }
}
