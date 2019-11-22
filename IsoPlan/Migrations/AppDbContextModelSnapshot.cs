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
                            PasswordHash = new byte[] { 52, 52, 63, 242, 230, 46, 33, 209, 166, 97, 120, 123, 140, 83, 230, 91, 184, 158, 53, 72, 251, 32, 62, 46, 84, 53, 213, 119, 4, 6, 114, 200, 48, 22, 105, 213, 176, 159, 18, 10, 66, 38, 230, 236, 205, 254, 179, 81, 23, 245, 3, 166, 132, 65, 125, 36, 44, 157, 196, 37, 89, 213, 241, 245 },
                            PasswordSalt = new byte[] { 54, 237, 92, 88, 61, 93, 164, 146, 78, 11, 245, 71, 100, 165, 24, 181, 186, 175, 64, 129, 102, 237, 227, 212, 43, 202, 15, 59, 13, 140, 42, 198, 157, 155, 130, 53, 116, 181, 39, 65, 148, 73, 239, 129, 248, 241, 96, 114, 132, 147, 223, 116, 116, 133, 209, 253, 101, 136, 130, 182, 222, 249, 101, 149, 122, 177, 107, 112, 145, 126, 72, 117, 114, 24, 21, 244, 63, 255, 10, 31, 168, 79, 228, 130, 130, 217, 27, 251, 142, 62, 162, 130, 195, 96, 52, 58, 110, 77, 224, 119, 107, 182, 7, 86, 143, 204, 127, 26, 157, 194, 107, 0, 57, 133, 60, 14, 159, 155, 199, 118, 206, 81, 86, 247, 189, 107, 217, 6 },
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
