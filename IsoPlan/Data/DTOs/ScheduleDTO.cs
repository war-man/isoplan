using System;

namespace IsoPlan.Data.DTOs
{
    public class ScheduleDTO
    {
        public int JobId { get; set; }
        public string JobName { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public double Salary { get; set; }
        public double Multiplier { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }
    }
}
