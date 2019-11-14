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
    [Migration("20191109191259_employeeNote")]
    partial class employeeNote
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

                    b.Property<DateTime>("WorkEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("WorkStart")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("IsoPlan.Data.Entities.Job", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClientEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClientName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClientNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("RGCollected")
                        .HasColumnType("bit");

                    b.Property<DateTime>("RGDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("IsoPlan.Data.Entities.Schedule", b =>
                {
                    b.Property<int>("ConstructionSiteId")
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

                    b.HasKey("ConstructionSiteId", "EmployeeId", "Date");

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
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "Milan",
                            LastName = "Milovanovic",
                            PasswordHash = new byte[] { 140, 81, 117, 11, 6, 91, 19, 23, 108, 8, 10, 62, 65, 113, 211, 46, 116, 205, 124, 27, 80, 35, 39, 137, 165, 126, 120, 23, 144, 103, 64, 11, 117, 134, 177, 92, 15, 186, 213, 41, 221, 152, 135, 3, 96, 118, 169, 60, 245, 198, 53, 251, 183, 14, 238, 78, 13, 2, 184, 56, 23, 10, 141, 201 },
                            PasswordSalt = new byte[] { 216, 74, 2, 137, 82, 246, 20, 174, 67, 165, 181, 205, 163, 71, 187, 191, 15, 240, 105, 165, 101, 71, 21, 159, 49, 193, 53, 114, 89, 170, 126, 61, 249, 170, 73, 105, 18, 162, 106, 154, 203, 232, 120, 194, 165, 129, 63, 38, 119, 159, 136, 209, 242, 73, 158, 69, 33, 59, 108, 99, 144, 15, 183, 37, 175, 172, 9, 66, 119, 189, 212, 145, 115, 81, 3, 184, 255, 127, 201, 33, 255, 18, 91, 146, 162, 211, 64, 166, 76, 237, 57, 129, 43, 86, 111, 11, 11, 165, 222, 77, 136, 20, 51, 117, 100, 45, 149, 83, 125, 131, 48, 216, 49, 1, 172, 191, 182, 182, 184, 237, 226, 217, 204, 111, 248, 236, 27, 50 },
                            Role = "Admin",
                            Username = "milan"
                        });
                });

            modelBuilder.Entity("IsoPlan.Data.Entities.Schedule", b =>
                {
                    b.HasOne("IsoPlan.Data.Entities.Job", "ConstructionSite")
                        .WithMany()
                        .HasForeignKey("ConstructionSiteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IsoPlan.Data.Entities.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}