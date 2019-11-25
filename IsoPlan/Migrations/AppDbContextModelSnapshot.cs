﻿// <auto-generated />
using System;
using IsoPlan.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IsoPlan.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
                            PasswordHash = new byte[] { 168, 229, 161, 22, 201, 21, 186, 45, 220, 100, 97, 138, 246, 255, 235, 81, 116, 69, 226, 88, 91, 97, 221, 78, 140, 177, 108, 76, 155, 255, 137, 0, 88, 113, 59, 133, 222, 170, 253, 62, 197, 220, 118, 87, 171, 165, 42, 140, 34, 149, 252, 202, 240, 60, 145, 141, 4, 81, 119, 169, 22, 36, 242, 211 },
                            PasswordSalt = new byte[] { 228, 99, 41, 156, 64, 216, 128, 46, 151, 1, 119, 77, 171, 7, 112, 237, 7, 199, 212, 197, 124, 62, 103, 64, 180, 163, 218, 65, 90, 252, 194, 220, 135, 69, 225, 131, 61, 246, 73, 32, 240, 130, 42, 227, 203, 164, 135, 249, 42, 174, 153, 241, 10, 26, 196, 136, 5, 75, 165, 133, 191, 51, 25, 253, 210, 10, 134, 125, 152, 184, 135, 44, 96, 192, 94, 66, 10, 114, 247, 218, 115, 231, 192, 162, 22, 197, 130, 160, 100, 113, 250, 11, 139, 48, 59, 223, 203, 205, 104, 255, 83, 92, 80, 239, 214, 216, 8, 225, 165, 39, 209, 2, 12, 118, 34, 231, 186, 96, 109, 224, 90, 230, 229, 245, 23, 57, 235, 126 },
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
