using System;

namespace IsoPlan.Data.DTOs
{
    public class ScheduleDTO
    {
        public int JobId { get; set; }
        public JobDTO Job { get; set; }
        public int EmployeeId { get; set; }
        public EmployeeDTO Employee { get; set; }
        public double Salary { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }
    }
}
