﻿using System;
using System.Collections.Generic;

namespace IsoPlan.Data.DTOs
{
    public class ScheduleAddDTO
    {
        public JobDTO Job { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }
        public List<EmployeeDTO> Employees { get; set; }
    }
}
