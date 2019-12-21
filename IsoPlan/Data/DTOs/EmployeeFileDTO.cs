using Microsoft.AspNetCore.Http;

namespace IsoPlan.Data.DTOs
{
    public class EmployeeFileDTO
    {
        public IFormFileCollection Files { get; set; }
        public int EmployeeId { get; set; }
        public string Folder { get; set; }
    }
}
