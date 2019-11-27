﻿// <auto-generated />
using System;
using IsoPlan.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IsoPlan.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20191127171521_job-items")]
    partial class jobitems
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("IsoPlan.Data.Entities.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContractType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Salary")
                        .HasColumnType("real");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("WorkEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("WorkStart")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("IsoPlan.Data.Entities.EmployeeFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("EmployeeFiles");
                });

            modelBuilder.Entity("IsoPlan.Data.Entities.Job", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClientContact")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClientName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DevisDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DevisStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("RGCollected")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("RGDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("TotalBuy")
                        .HasColumnType("real");

                    b.Property<float>("TotalProfit")
                        .HasColumnType("real");

                    b.Property<float>("TotalSell")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("IsoPlan.Data.Entities.JobItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("Buy")
                        .HasColumnType("real");

                    b.Property<int>("JobId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Profit")
                        .HasColumnType("real");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<float>("Sell")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("JobId");

                    b.ToTable("JobItem");
                });

            modelBuilder.Entity("IsoPlan.Data.Entities.Schedule", b =>
                {
                    b.Property<int>("JobId")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Salary")
                        .HasColumnType("float");

                    b.Property<int>("Team")
                        .HasColumnType("int");

                    b.HasKey("JobId", "EmployeeId", "Date");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("IsoPlan.Data.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasAlternateKey("Username")
                        .HasName("AlternateKey_Username");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "Milan",
                            LastName = "Milovanovic",
                            PasswordHash = new byte[] { 174, 107, 45, 208, 126, 6, 176, 52, 185, 158, 166, 255, 189, 238, 10, 161, 248, 5, 238, 19, 198, 54, 74, 103, 83, 187, 130, 30, 51, 42, 131, 52, 18, 226, 198, 122, 182, 34, 156, 69, 120, 255, 83, 238, 85, 92, 244, 215, 203, 50, 65, 104, 128, 16, 43, 236, 170, 199, 91, 130, 233, 251, 58, 75 },
                            PasswordSalt = new byte[] { 198, 22, 132, 196, 205, 92, 250, 242, 49, 149, 198, 194, 4, 61, 17, 235, 27, 39, 240, 72, 211, 136, 152, 102, 192, 101, 174, 120, 214, 188, 65, 10, 28, 193, 51, 107, 118, 185, 151, 99, 120, 37, 254, 197, 8, 212, 156, 31, 207, 234, 192, 52, 91, 178, 185, 186, 4, 25, 86, 191, 35, 58, 93, 210, 3, 36, 167, 155, 50, 183, 128, 130, 180, 229, 104, 146, 112, 27, 54, 97, 181, 80, 166, 143, 48, 47, 113, 146, 74, 147, 96, 157, 60, 103, 9, 229, 43, 31, 195, 1, 9, 184, 231, 193, 239, 185, 9, 149, 148, 67, 149, 5, 49, 3, 39, 108, 78, 45, 123, 140, 240, 246, 53, 11, 158, 140, 41, 99 },
                            Role = "Admin",
                            Username = "milan"
                        });
                });

            modelBuilder.Entity("IsoPlan.Data.Entities.EmployeeFile", b =>
                {
                    b.HasOne("IsoPlan.Data.Entities.Employee", "Employee")
                        .WithMany("Files")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IsoPlan.Data.Entities.JobItem", b =>
                {
                    b.HasOne("IsoPlan.Data.Entities.Job", "Job")
                        .WithMany("JobItems")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IsoPlan.Data.Entities.Schedule", b =>
                {
                    b.HasOne("IsoPlan.Data.Entities.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IsoPlan.Data.Entities.Job", "Job")
                        .WithMany()
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}