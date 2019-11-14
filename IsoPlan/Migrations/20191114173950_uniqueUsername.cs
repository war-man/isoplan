using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IsoPlan.Migrations
{
    public partial class uniqueUsername : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AlternateKey_Username",
                table: "Users",
                column: "Username");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 226, 203, 8, 248, 59, 123, 241, 6, 136, 56, 206, 94, 50, 36, 169, 114, 199, 219, 16, 25, 27, 147, 108, 152, 181, 39, 25, 17, 124, 84, 71, 14, 106, 223, 239, 118, 117, 1, 39, 134, 7, 120, 163, 160, 160, 190, 24, 61, 3, 151, 146, 82, 120, 95, 96, 14, 73, 176, 76, 178, 39, 206, 126, 199 }, new byte[] { 175, 124, 5, 69, 12, 36, 55, 90, 116, 247, 26, 50, 50, 87, 112, 9, 148, 27, 187, 99, 126, 60, 184, 175, 149, 2, 5, 193, 7, 191, 52, 235, 218, 10, 204, 162, 38, 218, 197, 58, 147, 95, 158, 225, 16, 59, 227, 131, 15, 8, 15, 5, 36, 67, 220, 148, 250, 13, 111, 68, 215, 170, 243, 224, 140, 46, 4, 13, 236, 228, 14, 106, 252, 41, 46, 172, 11, 183, 118, 115, 67, 102, 144, 193, 208, 174, 43, 149, 36, 247, 219, 28, 173, 165, 237, 234, 72, 50, 39, 43, 33, 92, 213, 42, 58, 118, 130, 248, 56, 16, 105, 196, 94, 140, 43, 143, 202, 199, 31, 189, 131, 214, 200, 187, 124, 17, 241, 182 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AlternateKey_Username",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 109, 157, 155, 80, 199, 126, 178, 206, 60, 178, 13, 217, 35, 207, 30, 179, 16, 246, 72, 159, 231, 19, 127, 128, 74, 204, 80, 67, 15, 138, 226, 181, 174, 35, 65, 123, 189, 182, 72, 185, 123, 147, 184, 209, 150, 3, 171, 230, 87, 159, 42, 6, 8, 102, 93, 144, 88, 213, 154, 20, 216, 5, 48, 254 }, new byte[] { 102, 240, 84, 169, 236, 239, 196, 254, 243, 194, 123, 84, 164, 214, 128, 63, 254, 84, 254, 184, 83, 159, 12, 17, 38, 140, 137, 119, 197, 110, 16, 47, 181, 96, 168, 206, 72, 232, 184, 61, 244, 183, 187, 28, 191, 133, 83, 188, 228, 29, 151, 19, 2, 20, 163, 107, 111, 53, 238, 190, 91, 151, 66, 20, 189, 142, 138, 233, 216, 42, 99, 97, 30, 246, 123, 15, 181, 218, 21, 63, 224, 137, 141, 105, 141, 9, 70, 190, 229, 178, 49, 202, 33, 222, 24, 213, 15, 210, 5, 68, 185, 15, 104, 122, 155, 175, 161, 142, 103, 92, 135, 236, 30, 32, 116, 45, 214, 215, 166, 179, 147, 135, 255, 133, 163, 107, 229, 197 } });
        }
    }
}
