using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsoPlan.Data.DTOs
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public float Salary { get; set; }
        public string AccountNumber { get; set; }
        public string ContractType { get; set; }
        public DateTime WorkStart { get; set; }
        public DateTime? WorkEnd { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
    }
}
