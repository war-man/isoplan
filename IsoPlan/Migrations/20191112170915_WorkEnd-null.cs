using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IsoPlan.Migrations
{
    public partial class WorkEndnull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "WorkEnd",
                table: "Employees",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 109, 157, 155, 80, 199, 126, 178, 206, 60, 178, 13, 217, 35, 207, 30, 179, 16, 246, 72, 159, 231, 19, 127, 128, 74, 204, 80, 67, 15, 138, 226, 181, 174, 35, 65, 123, 189, 182, 72, 185, 123, 147, 184, 209, 150, 3, 171, 230, 87, 159, 42, 6, 8, 102, 93, 144, 88, 213, 154, 20, 216, 5, 48, 254 }, new byte[] { 102, 240, 84, 169, 236, 239, 196, 254, 243, 194, 123, 84, 164, 214, 128, 63, 254, 84, 254, 184, 83, 159, 12, 17, 38, 140, 137, 119, 197, 110, 16, 47, 181, 96, 168, 206, 72, 232, 184, 61, 244, 183, 187, 28, 191, 133, 83, 188, 228, 29, 151, 19, 2, 20, 163, 107, 111, 53, 238, 190, 91, 151, 66, 20, 189, 142, 138, 233, 216, 42, 99, 97, 30, 246, 123, 15, 181, 218, 21, 63, 224, 137, 141, 105, 141, 9, 70, 190, 229, 178, 49, 202, 33, 222, 24, 213, 15, 210, 5, 68, 185, 15, 104, 122, 155, 175, 161, 142, 103, 92, 135, 236, 30, 32, 116, 45, 214, 215, 166, 179, 147, 135, 255, 133, 163, 107, 229, 197 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "WorkEnd",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 140, 81, 117, 11, 6, 91, 19, 23, 108, 8, 10, 62, 65, 113, 211, 46, 116, 205, 124, 27, 80, 35, 39, 137, 165, 126, 120, 23, 144, 103, 64, 11, 117, 134, 177, 92, 15, 186, 213, 41, 221, 152, 135, 3, 96, 118, 169, 60, 245, 198, 53, 251, 183, 14, 238, 78, 13, 2, 184, 56, 23, 10, 141, 201 }, new byte[] { 216, 74, 2, 137, 82, 246, 20, 174, 67, 165, 181, 205, 163, 71, 187, 191, 15, 240, 105, 165, 101, 71, 21, 159, 49, 193, 53, 114, 89, 170, 126, 61, 249, 170, 73, 105, 18, 162, 106, 154, 203, 232, 120, 194, 165, 129, 63, 38, 119, 159, 136, 209, 242, 73, 158, 69, 33, 59, 108, 99, 144, 15, 183, 37, 175, 172, 9, 66, 119, 189, 212, 145, 115, 81, 3, 184, 255, 127, 201, 33, 255, 18, 91, 146, 162, 211, 64, 166, 76, 237, 57, 129, 43, 86, 111, 11, 11, 165, 222, 77, 136, 20, 51, 117, 100, 45, 149, 83, 125, 131, 48, 216, 49, 1, 172, 191, 182, 182, 184, 237, 226, 217, 204, 111, 248, 236, 27, 50 } });
        }
    }
}
