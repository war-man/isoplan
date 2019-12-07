using IsoPlan.Data;
using IsoPlan.Data.Entities;
using IsoPlan.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsoPlan.Services
{
    public interface IScheduleService
    {
        IEnumerable<ScheduleWeek> GetWeeks(DateTime start);
        void Add(DateTime date, Job job, List<Employee> employees);
        Schedule GetById(int jobId, int employeeId, DateTime date);
        void Update(Schedule scheduleParam);
        void Delete(Schedule scheduleParam);

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

        public void Add(DateTime date, Job jobParam, List<Employee> employees)
        {
            Job job = _context.Jobs.Find(jobParam.Id);

            if(job == null)
            {
                throw new AppException("Job not found");
            }

            foreach (Employee e in employees)
            {
                if(_context.Employees.Find(e.Id) == null)
                {
                    throw new AppException("Employee not found");
                }
            }

            foreach (Employee e in employees)
            {
                _context.Schedules.Add(new Schedule
                {
                    EmployeeId = e.Id,
                    JobId = job.Id,
                    Date = date,
                    Salary = e.Salary                    
                });                
            }
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw new AppException("One of the schedules already exists");
            }
        }

        public Schedule GetById(int jobId, int employeeId, DateTime date)
        {
            return _context.Schedules.FirstOrDefault(s => 
                s.EmployeeId == employeeId &&
                s.JobId == jobId &&
                s.Date.Equals(date)
            );
        }

        public IEnumerable<ScheduleWeek> GetWeeks(DateTime start)
        {
            var activeEmployees = _employeeService.GetAll(EmployeeStatus.Active);

            List<ScheduleWeek> weeks = new List<ScheduleWeek>();

            foreach (Employee e in activeEmployees)
            {
                List<Schedule> employeeSchedules = _context.Schedules
                    .Include(s => s.Job)
                    .Where(s => s.EmployeeId == e.Id && s.Date >= start && s.Date <= start.AddDays(6))
                    .ToList();
                weeks.Add(new ScheduleWeek
                {
                    Name = e.FirstName + " " + e.LastName,
                    Date1 = employeeSchedules.Where(s => s.Date.Equals(start)).ToList(),
                    Date2 = employeeSchedules.Where(s => s.Date.Equals(start.AddDays(1))).ToList(),
                    Date3 = employeeSchedules.Where(s => s.Date.Equals(start.AddDays(2))).ToList(),
                    Date4 = employeeSchedules.Where(s => s.Date.Equals(start.AddDays(3))).ToList(),
                    Date5 = employeeSchedules.Where(s => s.Date.Equals(start.AddDays(4))).ToList(),
                    Date6 = employeeSchedules.Where(s => s.Date.Equals(start.AddDays(5))).ToList(),
                });
            }

            return weeks;
        }

        public void Update(Schedule scheduleParam)
        {
            Schedule schedule = GetById(scheduleParam.JobId, scheduleParam.EmployeeId, scheduleParam.Date);

            if(schedule == null)
            {
                throw new AppException("Schedule not found");
            }

            schedule.Note = scheduleParam.Note;
            schedule.Salary = scheduleParam.Salary;

            _context.SaveChanges();
        }

        public void Delete(Schedule scheduleParam)
        {
            Schedule schedule = GetById(scheduleParam.JobId, scheduleParam.EmployeeId, scheduleParam.Date);

            if (schedule == null)
            {
                throw new AppException("Schedule not found");
            }

            _context.Schedules.Remove(schedule);
            _context.SaveChanges();
        }
    }
}
