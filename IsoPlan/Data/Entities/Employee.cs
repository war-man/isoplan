using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsoPlan.Data.Entities
{
    public class Employee
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
        public List<EmployeeFile> Files { get; set; }
    }
}
