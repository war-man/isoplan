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
    public interface IJobService
    {
        IEnumerable<Job> GetAll();
        Job GetById(int id);
        void Create(Job job);
        void Update(Job job);
        void Delete(int id);
        void CreateJobItem(JobItem jobItem);
        void UpdateJobItem(JobItem jobItem);
        void DeleteJobItem(int id);
    }
    public class JobService : IJobService
    {
        private readonly AppDbContext _context;

        public JobService(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Job> GetAll()
        {
            return _context.Jobs.ToList();
        }

        public Job GetById(int id)
        {
            return _context.Jobs
                .Include(j => j.JobItems)
                .FirstOrDefault(j => j.Id == id);
        }        

        public void Create(Job job)
        {
            if (!ValidateJobData(job))
            {
                throw new AppException("Some required fields are empty");
            }

            _context.Jobs.Add(job);
            _context.SaveChanges();
        }

        public void Update(Job jobParam)
        {
            var job = _context.Jobs.Find(jobParam.Id);

            if (job == null)
            {
                throw new AppException("Job not found");
            }

            if (!ValidateJobData(job))
            {
                throw new AppException("Some required fields are empty");
            }

            job.Name = jobParam.Name;
            job.Address = jobParam.Address;
            job.ClientName = jobParam.ClientName;
            job.ClientContact = jobParam.ClientContact;
            job.DevisStatus = jobParam.DevisStatus;
            job.DevisDate = jobParam.DevisDate;
            job.StartDate = jobParam.StartDate;
            job.EndDate = jobParam.EndDate;
            job.RGDate = jobParam.RGDate;
            job.RGCollected = jobParam.RGCollected;
            job.Status = jobParam.Status;

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var job = _context.Jobs.Find(id);

            if (job == null)
            {
                throw new AppException("Job not found");
            }

            _context.Jobs.Remove(job);
            _context.SaveChanges();
        }

        private bool ValidateJobData(Job job)
        {
            return (
                !string.IsNullOrWhiteSpace(job.ClientName) &&
                !string.IsNullOrWhiteSpace(job.Name) &&
                JobStatus.JobStatusList.Contains(job.Status) &&
                DevisStatus.DevisStatusList.Contains(job.DevisStatus)
            );
        }

        private void recalculate(Job job)
        {
            job.TotalBuy = job.JobItems.Sum(ji => ji.Buy);
            job.TotalSell = job.JobItems.Sum(ji => ji.Sell);
            job.TotalProfit = job.JobItems.Sum(ji => ji.Profit);
            _context.SaveChanges();
        }

        public void CreateJobItem(JobItem jobItem)
        {
            var job = GetById(jobItem.JobId);

            if(job == null)
            {
                throw new AppException("Job not found");
            }

            // needs validation for empty fields
            _context.JobItems.Add(jobItem);
            _context.SaveChanges();

            recalculate(job);
        }

        public void UpdateJobItem(JobItem jobItemParam)
        {
            var jobItem = _context.JobItems.Find(jobItemParam.Id);

            if (jobItem == null)
            {
                throw new AppException("Job item not found");
            }

            var job = GetById(jobItem.JobId);

            if (job == null)
            {
                throw new AppException("Job not found");
            }

            // needs validation for empty fields
            jobItem.Name = jobItemParam.Name;
            jobItem.Quantity = jobItemParam.Quantity;
            jobItem.Buy = jobItemParam.Buy;
            jobItem.Sell = jobItemParam.Sell;
            jobItem.Profit = jobItemParam.Profit;
            _context.SaveChanges();

            recalculate(job);
        }

        public void DeleteJobItem(int id)
        {
            var jobItem = _context.JobItems.Find(id);

            if (jobItem == null)
            {
                throw new AppException("Job item not found");
            }

            var job = GetById(jobItem.JobId);

            if (job == null)
            {
                throw new AppException("Job not found");
            }

            _context.JobItems.Remove(jobItem);
            _context.SaveChanges();

            recalculate(job);
        }
    }
}
