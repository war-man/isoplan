using IsoPlan.Data;
using IsoPlan.Data.Entities;
using IsoPlan.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IsoPlan.Services
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetAll(string status);
        IEnumerable<Employee> GetbySchedules(DateTime startDate);
        Employee GetById(int id);
        void Create(Employee employee);
        void CreateFile(EmployeeFile employeeFile);
        EmployeeFile GetFile(int id);
        void DeleteFile(int id);
        List<EmployeeFile> GetFiles(int employeeId);
        void Update(Employee employee);
        void Delete(int id);
    }
    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext _context;

        public EmployeeService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Employee> GetAll(string status)
        {
            return _context.Employees
                .Where(e => string.IsNullOrWhiteSpace(status) || e.Status.Equals(status))
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .ToList();
        }

        public Employee GetById(int id)
        {
            return _context.Employees.Include(e => e.Files).SingleOrDefault(e => e.Id == id);
        }

        public void Create(Employee employee)
        {
            if (!ValidateEmployeeData(employee))
            {
                throw new AppException("Some required fields are empty");
            }
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        public void CreateFile(EmployeeFile employeeFile)
        {
            _context.EmployeeFiles.Add(employeeFile);
            _context.SaveChanges();
        }

        public EmployeeFile GetFile(int id)
        {
            return _context.EmployeeFiles.Find(id);
        }

        public void DeleteFile(int id)
        {
            EmployeeFile file = GetFile(id);
            if (file == null)
            {
                throw new AppException("File not found in database.");
            }            
            _context.EmployeeFiles.Remove(file);
            _context.SaveChanges();
        }

        public List<EmployeeFile> GetFiles(int employeeId)
        {
            var employee = _context.Employees.Find(employeeId);

            if (employee == null)
            {
                throw new AppException("Employee not found");
            }

            return _context.EmployeeFiles
                .Select(ef => new EmployeeFile
                {
                    Id = ef.Id,
                    Name = ef.Name,
                    EmployeeId = ef.EmployeeId
                })
                .Where(ef => ef.EmployeeId == employeeId)
                .OrderBy(ef => ef.Name)
                .ToList();
        }

        public void Update(Employee employeeParam)
        {
            var employee = _context.Employees.Find(employeeParam.Id);

            if (employee == null)
            {
                throw new AppException("Employee not found");
            }

            if (!ValidateEmployeeData(employeeParam))
            {
                throw new AppException("Some required fields are empty");
            }

            employee.FirstName = employeeParam.FirstName;
            employee.LastName = employeeParam.LastName;
            employee.Salary = employeeParam.Salary;
            employee.AccountNumber = employeeParam.AccountNumber;
            employee.ContractType = employeeParam.ContractType;
            employee.WorkStart = employeeParam.WorkStart;
            employee.WorkEnd = employeeParam.WorkEnd;
            employee.Status = employeeParam.Status;
            employee.Note = employeeParam.Note;

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var employee = GetById(id);

            if (employee == null)
            {
                throw new AppException("Employee not found");
            }

            _context.Employees.Remove(employee);
            _context.SaveChanges();

        }

        private bool ValidateEmployeeData(Employee employee)
        {
            return (
                !string.IsNullOrWhiteSpace(employee.FirstName) &&
                !string.IsNullOrWhiteSpace(employee.LastName) &&
                (employee.Salary >= 0) &&
                !string.IsNullOrWhiteSpace(employee.AccountNumber) &&
                ContractType.ContractTypeList.Contains(employee.ContractType) &&
                (employee.WorkStart != null) &&
                EmployeeStatus.EmployeeStatusList.Contains(employee.Status)
            );
        }

        public IEnumerable<Employee> GetbySchedules(DateTime startDate)
        {
            return _context.Schedules
                .Include(s => s.Employee)
                .Where(s => s.Date >= startDate && s.Date < startDate.AddMonths(1))
                .AsEnumerable()
                .GroupBy(s => s.Employee)
                .Select(group => group.Key)
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .ToList();
        }
    }
}
