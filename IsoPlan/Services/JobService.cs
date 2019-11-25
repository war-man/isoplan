using IsoPlan.Data;
using IsoPlan.Data.Entities;
using IsoPlan.Exceptions;
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
            return _context.Jobs.Find(id);
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
            var constructionSite = _context.Jobs.Find(id);

            if (constructionSite == null)
            {
                throw new AppException("Job not found");
            }

            _context.Jobs.Remove(constructionSite);
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
    }
}
