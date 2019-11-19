using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IsoPlan.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Salary = table.Column<float>(nullable: false),
                    AccountNumber = table.Column<string>(nullable: true),
                    ContractType = table.Column<string>(nullable: true),
                    WorkStart = table.Column<DateTime>(nullable: false),
                    WorkEnd = table.Column<DateTime>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    ClientName = table.Column<string>(nullable: true),
                    ClientNumber = table.Column<string>(nullable: true),
                    ClientEmail = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    RGDate = table.Column<DateTime>(nullable: false),
                    RGCollected = table.Column<bool>(nullable: false),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: false),
                    Role = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.UniqueConstraint("AlternateKey_Username", x => x.Username);
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
                        name: "FK_Schedules_Jobs_ConstructionSiteId",
                        column: x => x.ConstructionSiteId,
                        principalTable: "Jobs",
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
                values: new object[] { 1, "Milan", "Milovanovic", new byte[] { 38, 137, 158, 23, 150, 114, 140, 120, 247, 60, 252, 232, 79, 75, 36, 34, 110, 227, 81, 68, 88, 19, 196, 17, 75, 58, 180, 117, 232, 54, 196, 188, 99, 247, 184, 133, 14, 210, 222, 3, 14, 41, 46, 19, 158, 52, 79, 236, 35, 103, 247, 128, 63, 163, 49, 126, 165, 81, 21, 87, 188, 216, 189, 110 }, new byte[] { 9, 106, 121, 147, 243, 126, 119, 205, 93, 138, 41, 189, 121, 170, 113, 68, 215, 155, 118, 73, 122, 85, 38, 21, 18, 109, 225, 190, 106, 50, 159, 199, 84, 62, 206, 117, 248, 228, 225, 178, 250, 218, 183, 237, 70, 48, 28, 175, 187, 143, 106, 19, 198, 84, 16, 194, 194, 137, 62, 236, 131, 1, 6, 77, 13, 150, 64, 133, 184, 63, 151, 3, 120, 105, 127, 234, 185, 6, 159, 153, 7, 129, 71, 41, 192, 101, 137, 115, 200, 88, 140, 9, 123, 76, 106, 229, 234, 130, 83, 93, 130, 106, 84, 60, 169, 187, 33, 210, 24, 11, 214, 90, 6, 212, 25, 48, 71, 216, 216, 233, 252, 235, 240, 191, 215, 197, 135, 183 }, "Admin", "milan" });

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
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
