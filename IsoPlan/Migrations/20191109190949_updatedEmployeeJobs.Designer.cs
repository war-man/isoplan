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
    [Migration("20191109190949_updatedEmployeeJobs")]
    partial class updatedEmployeeJobs
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
                            PasswordHash = new byte[] { 149, 144, 247, 242, 65, 60, 242, 220, 232, 227, 238, 172, 248, 238, 248, 242, 189, 202, 176, 133, 123, 249, 169, 75, 47, 194, 193, 191, 94, 237, 75, 122, 65, 45, 122, 3, 253, 241, 230, 29, 146, 154, 144, 50, 76, 70, 145, 85, 15, 235, 97, 235, 97, 27, 75, 213, 139, 213, 205, 134, 254, 32, 62, 183 },
                            PasswordSalt = new byte[] { 107, 77, 114, 206, 137, 237, 131, 35, 99, 215, 217, 173, 2, 241, 248, 251, 80, 34, 194, 167, 115, 246, 31, 119, 70, 12, 130, 164, 139, 157, 243, 181, 130, 77, 21, 60, 25, 165, 32, 149, 81, 86, 200, 50, 157, 255, 187, 179, 236, 198, 58, 153, 196, 212, 112, 162, 157, 153, 122, 56, 40, 206, 71, 147, 0, 71, 112, 103, 245, 110, 204, 212, 120, 184, 181, 75, 131, 175, 244, 97, 123, 147, 119, 41, 30, 81, 171, 161, 145, 89, 110, 102, 137, 22, 27, 88, 114, 24, 196, 87, 13, 52, 4, 121, 166, 148, 120, 92, 238, 36, 213, 43, 39, 115, 63, 151, 74, 251, 10, 101, 138, 201, 149, 4, 32, 149, 128, 62 },
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
