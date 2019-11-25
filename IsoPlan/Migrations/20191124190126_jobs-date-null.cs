using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IsoPlan.Migrations
{
    public partial class jobsdatenull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Jobs",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RGDate",
                table: "Jobs",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Jobs",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DevisDate",
                table: "Jobs",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 168, 229, 161, 22, 201, 21, 186, 45, 220, 100, 97, 138, 246, 255, 235, 81, 116, 69, 226, 88, 91, 97, 221, 78, 140, 177, 108, 76, 155, 255, 137, 0, 88, 113, 59, 133, 222, 170, 253, 62, 197, 220, 118, 87, 171, 165, 42, 140, 34, 149, 252, 202, 240, 60, 145, 141, 4, 81, 119, 169, 22, 36, 242, 211 }, new byte[] { 228, 99, 41, 156, 64, 216, 128, 46, 151, 1, 119, 77, 171, 7, 112, 237, 7, 199, 212, 197, 124, 62, 103, 64, 180, 163, 218, 65, 90, 252, 194, 220, 135, 69, 225, 131, 61, 246, 73, 32, 240, 130, 42, 227, 203, 164, 135, 249, 42, 174, 153, 241, 10, 26, 196, 136, 5, 75, 165, 133, 191, 51, 25, 253, 210, 10, 134, 125, 152, 184, 135, 44, 96, 192, 94, 66, 10, 114, 247, 218, 115, 231, 192, 162, 22, 197, 130, 160, 100, 113, 250, 11, 139, 48, 59, 223, 203, 205, 104, 255, 83, 92, 80, 239, 214, 216, 8, 225, 165, 39, 209, 2, 12, 118, 34, 231, 186, 96, 109, 224, 90, 230, 229, 245, 23, 57, 235, 126 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Jobs",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RGDate",
                table: "Jobs",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Jobs",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DevisDate",
                table: "Jobs",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 59, 127, 68, 239, 51, 140, 230, 234, 176, 128, 40, 246, 59, 248, 114, 72, 236, 139, 180, 50, 184, 220, 144, 78, 85, 121, 224, 93, 59, 159, 153, 76, 16, 52, 111, 98, 23, 246, 53, 60, 158, 20, 89, 137, 65, 84, 96, 103, 80, 244, 86, 131, 234, 236, 222, 164, 217, 228, 197, 244, 200, 195, 210, 91 }, new byte[] { 243, 253, 131, 186, 63, 40, 157, 23, 170, 208, 245, 57, 79, 171, 53, 55, 81, 137, 241, 154, 87, 192, 118, 105, 226, 41, 174, 191, 91, 184, 166, 180, 84, 52, 144, 164, 11, 39, 145, 56, 16, 112, 207, 14, 161, 16, 95, 0, 161, 52, 116, 251, 204, 164, 68, 8, 183, 197, 157, 178, 35, 117, 63, 193, 228, 75, 55, 127, 50, 46, 11, 167, 105, 135, 25, 161, 30, 38, 255, 209, 252, 115, 63, 244, 74, 155, 156, 106, 198, 229, 206, 188, 109, 3, 84, 23, 105, 216, 179, 125, 73, 21, 92, 185, 185, 254, 35, 164, 246, 159, 46, 6, 176, 222, 144, 95, 245, 210, 31, 54, 13, 76, 250, 47, 74, 67, 167, 243 } });
        }
    }
}
