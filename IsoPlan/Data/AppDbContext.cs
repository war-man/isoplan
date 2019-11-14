﻿using IsoPlan.Data.Entities;
using IsoPlan.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsoPlan.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Schedule>()
                .HasKey(s => new { s.ConstructionSiteId, s.EmployeeId, s.Date });

            modelBuilder.Entity<User>().HasAlternateKey(u => u.Username).HasName("AlternateKey_Username");

            Hash.CreatePasswordHash("milan", out byte[] passwordHash, out byte[] passwordSalt);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Milan",
                    LastName = "Milovanovic",
                    Role = "Admin",
                    Username = "milan",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                }
            );
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
    }
}