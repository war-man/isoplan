using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsoPlan.Data.Entities
{
    public class Facture
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public float Value { get; set; }
        public bool Paid { get; set; }
        public DateTime DatePaid { get; set; }
        public string FilePath { get; set; }
        public int JobId { get; set; }
        public Job Job { get; set; }
    }
}
