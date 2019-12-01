using IsoPlan.Data;
using IsoPlan.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsoPlan.Services
{
    public interface IScheduleService
    {
        IEnumerable<ScheduleWeek> GetAll(DateTime from, DateTime to);
    }
    public class ScheduleService : IScheduleService
    {
        private readonly AppDbContext _context;
        private readonly IEmployeeService _employeeService;

        public ScheduleService(AppDbContext context, IEmployeeService employeeService)
        {
            _context = context;
            _employeeService = employeeService;
        }
        public IEnumerable<ScheduleWeek> GetAll(DateTime from, DateTime to)
        {
            var employees = _employeeService.GetAll(EmployeeStatus.Active);
            var employeeIdList = employees.Select(e => e.Id);




            throw new NotImplementedException();
        }
    }
}
