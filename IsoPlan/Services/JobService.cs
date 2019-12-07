using IsoPlan.Data;
using IsoPlan.Data.Entities;
using IsoPlan.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IsoPlan.Services
{
    public interface IJobService
    {
        IEnumerable<Job> GetAll(string status);
        Job GetById(int id);
        void Create(Job job);
        void Update(Job job);
        void Delete(int id);
        void CreateJobItem(JobItem jobItem);
        void UpdateJobItem(JobItem jobItem);
        void DeleteJobItem(int id);
        void CreateFile(JobFile jobFile);
        JobFile GetFile(int id);
        void DeleteFile(int id);
        List<JobFilePair> GetFiles(int jobId);
    }
    public class JobService : IJobService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public JobService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IEnumerable<Job> GetAll(string status)
        {
            return _context.Jobs
                .Where(j => string.IsNullOrWhiteSpace(status) || j.Status.Equals(status))
                .ToList();
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
            var job = _context.Jobs
                .Include(j => j.Files)
                .FirstOrDefault(j => j.Id == id);

            if (job == null)
            {
                throw new AppException("Job not found");
            }

            List<JobFile> files = new List<JobFile>(job.Files);

            for (int i = 0; i < files.Count; i++)
            {
                DeleteFile(files[i].Id);
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

        private void Recalculate(Job job)
        {
            job.TotalBuy = job.JobItems.Sum(ji => ji.Buy);
            job.TotalSell = job.JobItems.Sum(ji => ji.Sell);
            job.TotalProfit = job.JobItems.Sum(ji => ji.Profit);
            _context.SaveChanges();
        }

        public void CreateJobItem(JobItem jobItem)
        {
            var job = GetById(jobItem.JobId);

            if (job == null)
            {
                throw new AppException("Job not found");
            }

            if (string.IsNullOrWhiteSpace(jobItem.Name))
            {
                throw new AppException("Name is empty");
            }

            jobItem.Profit = jobItem.Sell - jobItem.Buy;
            _context.JobItems.Add(jobItem);
            _context.SaveChanges();

            Recalculate(job);
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

            if (string.IsNullOrWhiteSpace(jobItemParam.Name))
            {
                throw new AppException("Name is empty");
            }

            jobItem.Name = jobItemParam.Name;
            jobItem.Quantity = jobItemParam.Quantity;
            jobItem.Buy = jobItemParam.Buy;
            jobItem.Sell = jobItemParam.Sell;
            jobItem.Profit = jobItem.Sell - jobItem.Buy;
            _context.SaveChanges();

            Recalculate(job);
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

            Recalculate(job);
        }

        public void CreateFile(JobFile jobFile)
        {
            _context.JobFiles.Add(jobFile);
            _context.SaveChanges();
        }

        public JobFile GetFile(int id)
        {
            return _context.JobFiles.Find(id);
        }

        public void DeleteFile(int id)
        {
            JobFile file = GetFile(id);
            if (file == null)
            {
                throw new AppException("File not found in database.");
            }
            string webRootPath = _env.WebRootPath;
            string fullPath = Path.Combine(webRootPath, "..\\Files\\" + file.Path);
            File.Delete(fullPath);
            _context.JobFiles.Remove(file);
            _context.SaveChanges();
        }

        public List<JobFilePair> GetFiles(int jobId)
        {
            var job = _context.Jobs.Find(jobId);

            if (job == null)
            {
                throw new AppException("Job not found");
            }

            List<JobFilePair> files = new List<JobFilePair>();

            foreach (string folder in JobFolder.JobFolderList)
            {
                files.Add(new JobFilePair
                {
                    Header = folder,
                    Items = _context.JobFiles
                            .Select(jf => new JobFile
                            {
                                Id = jf.Id,
                                JobId = jf.JobId,
                                Name = jf.Name,
                                Folder = jf.Folder
                            }
                            )
                            .Where(jf => jf.Folder == folder && jf.JobId == jobId)
                            .ToList()
                }
                );
            }

            return files;
        }
    }
}
