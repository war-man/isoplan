using Microsoft.AspNetCore.Http;

namespace IsoPlan.Data.DTOs
{
    public class JobFileDTO
    {
        public IFormFileCollection Files { get; set; }
        public int JobId { get; set; }
        public string Folder { get; set; }
    }
}
