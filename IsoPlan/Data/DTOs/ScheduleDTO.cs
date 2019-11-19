using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsoPlan.Data.DTOs
{
    public class ScheduleDTO
    {
        public JobDTO Job { get; set; }
        public EmployeeDTO Employee { get; set; }
        public double Salary { get; set; }
        public DateTime Date { get; set; }
        public int Team { get; set; }
        public string Note { get; set; }
    }
}
