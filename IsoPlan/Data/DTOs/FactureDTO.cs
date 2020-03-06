using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsoPlan.Data.DTOs
{
    public class FactureDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public float Value { get; set; }
        public bool Paid { get; set; }
        public string FilePath { get; set; }
        public int JobId { get; set; }
    }
}
