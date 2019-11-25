using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IsoPlan.Migrations
{
    public partial class jobs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientEmail",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "ClientNumber",
                table: "Jobs");

            migrationBuilder.AddColumn<string>(
                name: "ClientContact",
                table: "Jobs",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DevisDate",
                table: "Jobs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DevisStatus",
                table: "Jobs",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "TotalBuy",
                table: "Jobs",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "TotalProfit",
                table: "Jobs",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "TotalSell",
                table: "Jobs",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 59, 127, 68, 239, 51, 140, 230, 234, 176, 128, 40, 246, 59, 248, 114, 72, 236, 139, 180, 50, 184, 220, 144, 78, 85, 121, 224, 93, 59, 159, 153, 76, 16, 52, 111, 98, 23, 246, 53, 60, 158, 20, 89, 137, 65, 84, 96, 103, 80, 244, 86, 131, 234, 236, 222, 164, 217, 228, 197, 244, 200, 195, 210, 91 }, new byte[] { 243, 253, 131, 186, 63, 40, 157, 23, 170, 208, 245, 57, 79, 171, 53, 55, 81, 137, 241, 154, 87, 192, 118, 105, 226, 41, 174, 191, 91, 184, 166, 180, 84, 52, 144, 164, 11, 39, 145, 56, 16, 112, 207, 14, 161, 16, 95, 0, 161, 52, 116, 251, 204, 164, 68, 8, 183, 197, 157, 178, 35, 117, 63, 193, 228, 75, 55, 127, 50, 46, 11, 167, 105, 135, 25, 161, 30, 38, 255, 209, 252, 115, 63, 244, 74, 155, 156, 106, 198, 229, 206, 188, 109, 3, 84, 23, 105, 216, 179, 125, 73, 21, 92, 185, 185, 254, 35, 164, 246, 159, 46, 6, 176, 222, 144, 95, 245, 210, 31, 54, 13, 76, 250, 47, 74, 67, 167, 243 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientContact",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "DevisDate",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "DevisStatus",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "TotalBuy",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "TotalProfit",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "TotalSell",
                table: "Jobs");

            migrationBuilder.AddColumn<string>(
                name: "ClientEmail",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientNumber",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 52, 52, 63, 242, 230, 46, 33, 209, 166, 97, 120, 123, 140, 83, 230, 91, 184, 158, 53, 72, 251, 32, 62, 46, 84, 53, 213, 119, 4, 6, 114, 200, 48, 22, 105, 213, 176, 159, 18, 10, 66, 38, 230, 236, 205, 254, 179, 81, 23, 245, 3, 166, 132, 65, 125, 36, 44, 157, 196, 37, 89, 213, 241, 245 }, new byte[] { 54, 237, 92, 88, 61, 93, 164, 146, 78, 11, 245, 71, 100, 165, 24, 181, 186, 175, 64, 129, 102, 237, 227, 212, 43, 202, 15, 59, 13, 140, 42, 198, 157, 155, 130, 53, 116, 181, 39, 65, 148, 73, 239, 129, 248, 241, 96, 114, 132, 147, 223, 116, 116, 133, 209, 253, 101, 136, 130, 182, 222, 249, 101, 149, 122, 177, 107, 112, 145, 126, 72, 117, 114, 24, 21, 244, 63, 255, 10, 31, 168, 79, 228, 130, 130, 217, 27, 251, 142, 62, 162, 130, 195, 96, 52, 58, 110, 77, 224, 119, 107, 182, 7, 86, 143, 204, 127, 26, 157, 194, 107, 0, 57, 133, 60, 14, 159, 155, 199, 118, 206, 81, 86, 247, 189, 107, 217, 6 } });
        }
    }
}
