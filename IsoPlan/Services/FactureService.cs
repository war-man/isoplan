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
    public interface IFactureService
    {
        IEnumerable<Facture> GetAll(int JobId, string startDate, string endDate);
        Facture GetById(int id);
        void Create(Facture facture);
        void Update(Facture facture);
        void Delete(int id);
    }
    public class FactureService : IFactureService
    {
        private readonly AppDbContext _context;
        private readonly IJobService _jobService;

        public FactureService(AppDbContext context, IJobService jobService)
        {
            _context = context;
            _jobService = jobService;
        }
        public void Create(Facture facture)
        {
            if (string.IsNullOrWhiteSpace(facture.Name))
            {
                throw new AppException("Name is empty");
            }

            Job job = _jobService.GetById(facture.JobId);

            if(job == null)
            {
                throw new AppException("Job not found");
            }

            facture.FilePath = "";
            _context.Factures.Add(facture);
            _context.SaveChanges();

            job = _jobService.GetById(facture.JobId);
            _jobService.RecalculateFactures(job);
        }

        public void Delete(int id)
        {
            Facture facture = GetById(id);
            if(facture == null)
            {
                throw new AppException("Facture not found");
            }
            _context.Factures.Remove(facture);
            _context.SaveChanges();

            Job job = _jobService.GetById(facture.JobId);
            _jobService.RecalculateFactures(job);
        }

        public IEnumerable<Facture> GetAll(int JobId, string startDate, string endDate)
        {
            return _context.Factures
               .Where(f => (JobId == 0 || f.JobId == JobId) &&
                           (string.IsNullOrWhiteSpace(startDate) || f.Date >= DateTime.Parse(startDate)) &&
                           (string.IsNullOrWhiteSpace(endDate) || f.Date < DateTime.Parse(endDate)))
               .OrderByDescending(f => f.Date)
               .ToList();
        }

        public Facture GetById(int id)
        {
            return _context.Factures.Include(f => f.Job).FirstOrDefault(f => f.Id == id);
        }

        public void Update(Facture factureParam)
        {
            Facture facture = GetById(factureParam.Id);

            if(facture == null)
            {
                throw new AppException("Facture not found");
            }

            if (string.IsNullOrWhiteSpace(factureParam.Name))
            {
                throw new AppException("Name is empty");
            }

            facture.Name = factureParam.Name;
            facture.Date = factureParam.Date;
            facture.Value = factureParam.Value;
            facture.Paid = factureParam.Paid;
            _context.SaveChanges();

            Job job = _jobService.GetById(facture.JobId);            
            _jobService.RecalculateFactures(job);
        }
    }
}
