using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IsoPlan.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConstructionSites",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Client = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    RGDate = table.Column<DateTime>(nullable: false),
                    RGCollected = table.Column<bool>(nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstructionSites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Salary = table.Column<float>(nullable: false),
                    WorkStart = table.Column<DateTime>(nullable: false),
                    WorkEnd = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    ConstructionSiteId = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Salary = table.Column<double>(nullable: false),
                    Team = table.Column<int>(nullable: false),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => new { x.ConstructionSiteId, x.EmployeeId, x.Date });
                    table.ForeignKey(
                        name: "FK_Schedules_ConstructionSites_ConstructionSiteId",
                        column: x => x.ConstructionSiteId,
                        principalTable: "ConstructionSites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedules_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "PasswordHash", "PasswordSalt", "Role", "Username" },
                values: new object[] { 1, "Milan", "Milovanovic", new byte[] { 152, 172, 52, 243, 110, 140, 163, 122, 236, 11, 118, 232, 203, 57, 183, 107, 23, 77, 119, 25, 98, 183, 141, 10, 157, 194, 179, 77, 89, 12, 240, 44, 166, 188, 181, 218, 218, 202, 213, 69, 7, 67, 25, 15, 83, 143, 249, 1, 231, 72, 211, 143, 242, 78, 219, 130, 38, 121, 137, 119, 225, 172, 154, 22 }, new byte[] { 142, 36, 208, 161, 16, 105, 125, 67, 199, 99, 238, 2, 148, 220, 153, 64, 212, 132, 197, 146, 212, 158, 29, 99, 253, 248, 44, 11, 128, 160, 61, 63, 188, 167, 203, 247, 154, 241, 132, 181, 163, 62, 161, 205, 18, 64, 34, 129, 74, 120, 47, 193, 29, 217, 202, 190, 71, 73, 204, 202, 50, 69, 181, 71, 89, 29, 251, 76, 132, 208, 159, 44, 217, 39, 68, 46, 167, 157, 131, 234, 58, 103, 188, 156, 147, 107, 216, 106, 181, 27, 187, 58, 218, 212, 254, 154, 93, 72, 108, 247, 163, 24, 6, 65, 170, 210, 75, 81, 233, 38, 155, 237, 196, 207, 218, 200, 158, 149, 28, 181, 128, 188, 68, 86, 12, 155, 134, 15 }, "Admin", "milan" });

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_EmployeeId",
                table: "Schedules",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ConstructionSites");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
