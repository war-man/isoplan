using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsoPlan.Data.Entities
{
    public class Schedule
    {
        public int JobId { get; set; }
        public Job Job { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public double Salary { get; set; }
        public DateTime Date { get; set; }
        public int Team { get; set; }
        public string Note { get; set; }
    }
}
