using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IsoPlan.Migrations
{
    public partial class updatedEmployeeJobs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_ConstructionSites_ConstructionSiteId",
                table: "Schedules");

            migrationBuilder.DropTable(
                name: "ConstructionSites");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "AccountNumber",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContractType",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Employees",
                nullable: true);

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

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 149, 144, 247, 242, 65, 60, 242, 220, 232, 227, 238, 172, 248, 238, 248, 242, 189, 202, 176, 133, 123, 249, 169, 75, 47, 194, 193, 191, 94, 237, 75, 122, 65, 45, 122, 3, 253, 241, 230, 29, 146, 154, 144, 50, 76, 70, 145, 85, 15, 235, 97, 235, 97, 27, 75, 213, 139, 213, 205, 134, 254, 32, 62, 183 }, new byte[] { 107, 77, 114, 206, 137, 237, 131, 35, 99, 215, 217, 173, 2, 241, 248, 251, 80, 34, 194, 167, 115, 246, 31, 119, 70, 12, 130, 164, 139, 157, 243, 181, 130, 77, 21, 60, 25, 165, 32, 149, 81, 86, 200, 50, 157, 255, 187, 179, 236, 198, 58, 153, 196, 212, 112, 162, 157, 153, 122, 56, 40, 206, 71, 147, 0, 71, 112, 103, 245, 110, 204, 212, 120, 184, 181, 75, 131, 175, 244, 97, 123, 147, 119, 41, 30, 81, 171, 161, 145, 89, 110, 102, 137, 22, 27, 88, 114, 24, 196, 87, 13, 52, 4, 121, 166, 148, 120, 92, 238, 36, 213, 43, 39, 115, 63, 151, 74, 251, 10, 101, 138, 201, 149, 4, 32, 149, 128, 62 } });

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Jobs_ConstructionSiteId",
                table: "Schedules",
                column: "ConstructionSiteId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Jobs_ConstructionSiteId",
                table: "Schedules");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropColumn(
                name: "AccountNumber",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ContractType",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Employees");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Employees",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ConstructionSites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Client = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RGCollected = table.Column<bool>(type: "bit", nullable: false),
                    RGDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstructionSites", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 152, 172, 52, 243, 110, 140, 163, 122, 236, 11, 118, 232, 203, 57, 183, 107, 23, 77, 119, 25, 98, 183, 141, 10, 157, 194, 179, 77, 89, 12, 240, 44, 166, 188, 181, 218, 218, 202, 213, 69, 7, 67, 25, 15, 83, 143, 249, 1, 231, 72, 211, 143, 242, 78, 219, 130, 38, 121, 137, 119, 225, 172, 154, 22 }, new byte[] { 142, 36, 208, 161, 16, 105, 125, 67, 199, 99, 238, 2, 148, 220, 153, 64, 212, 132, 197, 146, 212, 158, 29, 99, 253, 248, 44, 11, 128, 160, 61, 63, 188, 167, 203, 247, 154, 241, 132, 181, 163, 62, 161, 205, 18, 64, 34, 129, 74, 120, 47, 193, 29, 217, 202, 190, 71, 73, 204, 202, 50, 69, 181, 71, 89, 29, 251, 76, 132, 208, 159, 44, 217, 39, 68, 46, 167, 157, 131, 234, 58, 103, 188, 156, 147, 107, 216, 106, 181, 27, 187, 58, 218, 212, 254, 154, 93, 72, 108, 247, 163, 24, 6, 65, 170, 210, 75, 81, 233, 38, 155, 237, 196, 207, 218, 200, 158, 149, 28, 181, 128, 188, 68, 86, 12, 155, 134, 15 } });

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_ConstructionSites_ConstructionSiteId",
                table: "Schedules",
                column: "ConstructionSiteId",
                principalTable: "ConstructionSites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
