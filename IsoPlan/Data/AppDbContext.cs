using IsoPlan.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace IsoPlan.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeFile>()
               .HasOne(ef => ef.Employee)
               .WithMany(e => e.Files)
               .HasForeignKey(ef => ef.EmployeeId);

            modelBuilder.Entity<JobFile>()
              .HasOne(jf => jf.Job)
              .WithMany(j => j.Files)
              .HasForeignKey(jf => jf.JobId);

            modelBuilder.Entity<JobItem>()
              .HasOne(ji => ji.Job)
              .WithMany(j => j.JobItems)
              .HasForeignKey(ji => ji.JobId);

            modelBuilder.Entity<Facture>()
              .HasOne(f => f.Job)
              .WithMany(j => j.Factures)
              .HasForeignKey(f => f.JobId);

            modelBuilder.Entity<User>().HasAlternateKey(u => u.Username).HasName("AlternateKey_Username");

            modelBuilder.Entity<Schedule>().HasKey(s => new { s.JobId, s.EmployeeId, s.Date });
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<EmployeeFile> EmployeeFiles { get; set; }
        public DbSet<JobItem> JobItems { get; set; }
        public DbSet<JobFile> JobFiles { get; set; }
        public DbSet<Facture> Factures { get; set; }

    }
}
