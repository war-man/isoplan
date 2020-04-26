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
    public interface IExpenseService
    {
        IEnumerable<Expense> GetAll(int JobId, string startDate, string endDate);
        Expense GetById(int id);
        void Create(Expense expense);
        void Update(Expense expense);
        void Delete(int id);
    }
    public class ExpenseService : IExpenseService
    {
        private readonly AppDbContext _context;
        private readonly IJobService _jobService;

        public ExpenseService(AppDbContext context, IJobService jobService)
        {
            _context = context;
            _jobService = jobService;
        }

        public void Create(Expense expense)
        {
            if (string.IsNullOrWhiteSpace(expense.Name))
            {
                throw new AppException("Name is empty");
            }

            JobItem jobItem = _jobService.GetJobItem(expense.JobItemId);

            if(jobItem == null)
            {
                throw new AppException("Job item not found");
            }

            expense.FilePath = "";
            _context.Expenses.Add(expense);
            _context.SaveChanges();

            jobItem = _jobService.GetJobItem(expense.JobItemId);
            _jobService.RecalculateExpenseForItem(jobItem);
        }

        public void Delete(int id)
        {
            Expense expense = GetById(id);

            if (expense == null)
            {
                throw new AppException("Expense not found");
            }

            _context.Expenses.Remove(expense);
            _context.SaveChanges();

            JobItem jobItem = _jobService.GetJobItem(expense.JobItemId);
            _jobService.RecalculateExpenseForItem(jobItem);
        }

        public IEnumerable<Expense> GetAll(int JobId, string startDate, string endDate)
        {
            return _context.Expenses
                          .Where(e => (JobId == 0 || e.JobId == JobId) &&
                                      (string.IsNullOrWhiteSpace(startDate) || e.Date >= DateTime.Parse(startDate)) &&
                                      (string.IsNullOrWhiteSpace(endDate) || e.Date < DateTime.Parse(endDate)))
                          .OrderByDescending(f => f.Date)
                          .ToList();
        }

        public Expense GetById(int id)
        {
            return _context.Expenses.FirstOrDefault(e => e.Id == id);
        }

        public void Update(Expense expenseParam)
        {
            Expense expense = GetById(expenseParam.Id);

            if (expense == null)
            {
                throw new AppException("Expense not found");
            }

            if (string.IsNullOrWhiteSpace(expenseParam.Name))
            {
                throw new AppException("Name is empty");
            }

            JobItem jobItem = _jobService.GetJobItem(expenseParam.JobItemId);
            if (jobItem == null)
            {
                throw new AppException("Job item not found");
            }

            JobItem oldJobItem = _jobService.GetJobItem(expense.JobItemId);            

            expense.Name = expenseParam.Name;
            expense.Date = expenseParam.Date;
            expense.Value = expenseParam.Value;
            expense.JobItemId = jobItem.Id;
            expense.JobItem = jobItem;

            _context.SaveChanges();            
            _jobService.RecalculateExpenseForItem(jobItem);
            if (oldJobItem.Id != jobItem.Id)
            {
                _jobService.RecalculateExpenseForItem(oldJobItem);
            }
        }
    }
}
