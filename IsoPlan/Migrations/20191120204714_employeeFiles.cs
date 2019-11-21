using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IsoPlan.Migrations
{
    public partial class employeeFiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Jobs_ConstructionSiteId",
                table: "Schedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Schedules",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "ConstructionSiteId",
                table: "Schedules");

            migrationBuilder.AddColumn<int>(
                name: "JobId",
                table: "Schedules",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Schedules",
                table: "Schedules",
                columns: new[] { "JobId", "EmployeeId", "Date" });

            migrationBuilder.CreateTable(
                name: "EmployeeFiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    EmployeeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeFiles_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 217, 78, 70, 77, 228, 191, 220, 9, 149, 86, 105, 166, 113, 72, 82, 248, 185, 204, 45, 49, 254, 240, 155, 138, 206, 130, 142, 169, 105, 96, 122, 161, 110, 133, 154, 5, 151, 104, 17, 227, 46, 202, 252, 99, 119, 66, 76, 174, 196, 129, 202, 216, 128, 89, 206, 105, 114, 43, 154, 143, 129, 209, 245, 138 }, new byte[] { 176, 6, 24, 207, 181, 114, 166, 89, 30, 113, 86, 117, 174, 36, 169, 133, 242, 251, 111, 27, 239, 109, 69, 219, 164, 234, 198, 188, 133, 243, 203, 125, 75, 176, 180, 154, 116, 47, 58, 84, 197, 102, 233, 38, 87, 35, 186, 235, 23, 173, 39, 110, 223, 138, 31, 195, 61, 238, 240, 25, 196, 241, 161, 189, 146, 238, 20, 78, 75, 225, 193, 102, 206, 15, 245, 242, 14, 23, 168, 40, 252, 82, 96, 223, 159, 158, 25, 54, 227, 35, 215, 57, 190, 102, 227, 5, 114, 243, 165, 144, 235, 54, 63, 135, 209, 237, 11, 172, 74, 118, 88, 6, 244, 115, 167, 34, 88, 255, 6, 10, 176, 151, 94, 185, 29, 17, 227, 187 } });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeFiles_EmployeeId",
                table: "EmployeeFiles",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Jobs_JobId",
                table: "Schedules",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Jobs_JobId",
                table: "Schedules");

            migrationBuilder.DropTable(
                name: "EmployeeFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Schedules",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "Schedules");

            migrationBuilder.AddColumn<int>(
                name: "ConstructionSiteId",
                table: "Schedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Schedules",
                table: "Schedules",
                columns: new[] { "ConstructionSiteId", "EmployeeId", "Date" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 38, 137, 158, 23, 150, 114, 140, 120, 247, 60, 252, 232, 79, 75, 36, 34, 110, 227, 81, 68, 88, 19, 196, 17, 75, 58, 180, 117, 232, 54, 196, 188, 99, 247, 184, 133, 14, 210, 222, 3, 14, 41, 46, 19, 158, 52, 79, 236, 35, 103, 247, 128, 63, 163, 49, 126, 165, 81, 21, 87, 188, 216, 189, 110 }, new byte[] { 9, 106, 121, 147, 243, 126, 119, 205, 93, 138, 41, 189, 121, 170, 113, 68, 215, 155, 118, 73, 122, 85, 38, 21, 18, 109, 225, 190, 106, 50, 159, 199, 84, 62, 206, 117, 248, 228, 225, 178, 250, 218, 183, 237, 70, 48, 28, 175, 187, 143, 106, 19, 198, 84, 16, 194, 194, 137, 62, 236, 131, 1, 6, 77, 13, 150, 64, 133, 184, 63, 151, 3, 120, 105, 127, 234, 185, 6, 159, 153, 7, 129, 71, 41, 192, 101, 137, 115, 200, 88, 140, 9, 123, 76, 106, 229, 234, 130, 83, 93, 130, 106, 84, 60, 169, 187, 33, 210, 24, 11, 214, 90, 6, 212, 25, 48, 71, 216, 216, 233, 252, 235, 240, 191, 215, 197, 135, 183 } });

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Jobs_ConstructionSiteId",
                table: "Schedules",
                column: "ConstructionSiteId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
