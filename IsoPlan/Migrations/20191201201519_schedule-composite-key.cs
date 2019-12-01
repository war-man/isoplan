using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IsoPlan.Migrations
{
    public partial class schedulecompositekey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Schedules",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_JobId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Schedules");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Schedules",
                table: "Schedules",
                columns: new[] { "JobId", "EmployeeId", "Date" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 224, 40, 59, 31, 21, 87, 174, 33, 76, 83, 191, 145, 125, 28, 122, 223, 64, 29, 80, 212, 146, 42, 143, 157, 192, 142, 220, 194, 154, 146, 74, 67, 78, 94, 196, 236, 36, 204, 61, 132, 8, 209, 173, 200, 216, 126, 246, 201, 208, 35, 120, 6, 176, 111, 218, 192, 98, 113, 36, 70, 54, 57, 169, 185 }, new byte[] { 35, 112, 26, 205, 77, 89, 13, 28, 84, 203, 212, 146, 111, 24, 39, 221, 64, 152, 47, 241, 194, 97, 235, 157, 145, 131, 243, 180, 9, 243, 188, 217, 15, 99, 249, 201, 120, 188, 122, 1, 107, 237, 47, 45, 59, 215, 190, 26, 171, 95, 72, 230, 80, 122, 9, 114, 7, 66, 85, 249, 168, 140, 201, 39, 104, 118, 68, 60, 95, 206, 244, 142, 90, 151, 179, 129, 114, 166, 130, 108, 118, 158, 16, 135, 106, 240, 60, 194, 146, 201, 240, 83, 242, 40, 175, 201, 87, 54, 237, 237, 155, 145, 148, 245, 65, 231, 229, 77, 181, 254, 191, 226, 101, 23, 211, 183, 87, 64, 11, 162, 242, 144, 33, 222, 97, 133, 55, 100 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Schedules",
                table: "Schedules");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Schedules",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Schedules",
                table: "Schedules",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 11, 124, 238, 249, 88, 224, 8, 197, 111, 160, 157, 166, 150, 251, 194, 213, 67, 58, 35, 175, 240, 137, 55, 15, 203, 27, 125, 96, 212, 29, 226, 174, 208, 63, 76, 165, 106, 140, 91, 133, 99, 45, 116, 129, 59, 167, 207, 110, 254, 25, 59, 141, 204, 148, 16, 119, 207, 183, 249, 246, 247, 19, 167, 140 }, new byte[] { 121, 45, 200, 99, 38, 204, 175, 4, 12, 211, 94, 108, 69, 91, 112, 198, 180, 156, 165, 231, 118, 248, 82, 29, 135, 34, 216, 28, 23, 210, 172, 198, 110, 44, 189, 184, 118, 219, 98, 67, 137, 164, 105, 90, 251, 66, 30, 69, 238, 127, 29, 100, 181, 47, 32, 52, 205, 170, 38, 104, 59, 135, 28, 255, 199, 226, 111, 200, 201, 159, 178, 136, 171, 229, 116, 209, 221, 127, 44, 110, 66, 99, 34, 146, 166, 20, 2, 90, 139, 203, 144, 79, 196, 130, 143, 91, 181, 31, 56, 148, 35, 130, 23, 53, 89, 91, 15, 165, 12, 85, 32, 217, 70, 59, 88, 143, 102, 76, 19, 141, 113, 135, 184, 154, 254, 245, 32, 143 } });

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_JobId",
                table: "Schedules",
                column: "JobId");
        }
    }
}
