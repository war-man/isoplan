using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsoPlan.Data.DTOs
{
    public class JobItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public float Buy { get; set; }
        public float Sell { get; set; }
        public float Profit { get; set; }
        public int JobId { get; set; }
    }
}
