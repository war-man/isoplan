using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsoPlan.Data.DTOs
{
    public class JobDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ClientName { get; set; }
        public string ClientNumber { get; set; }
        public string ClientEmail { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime RGDate { get; set; }
        public bool RGCollected { get; set; }        
        public bool Active { get; set; }
    }
}
