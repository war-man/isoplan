using Microsoft.AspNetCore.Http;

namespace IsoPlan.Data.DTOs
{
    public class JobFileDTO
    {
        public IFormFile File { get; set; }
        public int JobId { get; set; }
        public string Folder { get; set; }
    }
}
