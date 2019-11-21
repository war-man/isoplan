using IsoPlan.Data;
using IsoPlan.Data.Entities;
using IsoPlan.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsoPlan.Services
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetAll();
        Employee GetById(int id);
        void Create(Employee employee);
        void CreateFile(EmployeeFile employeeFile);
        EmployeeFile GetFile(int id);
        void DeleteFile(EmployeeFile employeeFile);
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

        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees.ToList();
        }

        public Employee GetById(int id)
        {
            return _context.Employees.Find(id);
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

        public void DeleteFile(EmployeeFile employeeFile)
        {
            _context.EmployeeFiles.Remove(employeeFile);
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
            var employee = _context.Employees.Find(id);

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
    }
}
