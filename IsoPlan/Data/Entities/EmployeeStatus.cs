﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsoPlan.Data.Entities
{
    public class EmployeeStatus
    {
        public const string Active = "Active";
        public const string Inactive = "Inactive";
        public const string Vacation = "Vacation";
        public const string Sick = "Sick";

        public static List<string> EmployeeStatusList = new List<string> { Active, Inactive, Vacation, Sick };
    }
}
