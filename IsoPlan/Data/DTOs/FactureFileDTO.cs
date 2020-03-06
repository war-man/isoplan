using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsoPlan.Data.DTOs
{
    public class FactureFileDTO
    {
        public IFormFile File{ get; set; }
        public int Id { get; set; }
    }
}
