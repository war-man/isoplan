﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IsoPlan.Migrations
{
    public partial class filesonetomany2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 52, 52, 63, 242, 230, 46, 33, 209, 166, 97, 120, 123, 140, 83, 230, 91, 184, 158, 53, 72, 251, 32, 62, 46, 84, 53, 213, 119, 4, 6, 114, 200, 48, 22, 105, 213, 176, 159, 18, 10, 66, 38, 230, 236, 205, 254, 179, 81, 23, 245, 3, 166, 132, 65, 125, 36, 44, 157, 196, 37, 89, 213, 241, 245 }, new byte[] { 54, 237, 92, 88, 61, 93, 164, 146, 78, 11, 245, 71, 100, 165, 24, 181, 186, 175, 64, 129, 102, 237, 227, 212, 43, 202, 15, 59, 13, 140, 42, 198, 157, 155, 130, 53, 116, 181, 39, 65, 148, 73, 239, 129, 248, 241, 96, 114, 132, 147, 223, 116, 116, 133, 209, 253, 101, 136, 130, 182, 222, 249, 101, 149, 122, 177, 107, 112, 145, 126, 72, 117, 114, 24, 21, 244, 63, 255, 10, 31, 168, 79, 228, 130, 130, 217, 27, 251, 142, 62, 162, 130, 195, 96, 52, 58, 110, 77, 224, 119, 107, 182, 7, 86, 143, 204, 127, 26, 157, 194, 107, 0, 57, 133, 60, 14, 159, 155, 199, 118, 206, 81, 86, 247, 189, 107, 217, 6 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 231, 194, 36, 191, 118, 204, 43, 112, 23, 123, 1, 210, 143, 169, 219, 17, 196, 138, 150, 182, 25, 188, 28, 253, 92, 53, 212, 145, 191, 60, 82, 141, 215, 117, 50, 152, 41, 176, 119, 65, 255, 241, 247, 50, 102, 46, 235, 133, 93, 193, 185, 12, 195, 93, 209, 127, 85, 81, 122, 9, 194, 253, 149, 227 }, new byte[] { 9, 219, 160, 249, 120, 21, 210, 103, 71, 37, 2, 91, 240, 131, 239, 70, 167, 36, 9, 232, 194, 69, 94, 32, 87, 129, 106, 101, 162, 136, 22, 94, 59, 61, 149, 174, 126, 110, 204, 226, 52, 89, 194, 89, 34, 152, 30, 167, 103, 235, 164, 202, 75, 164, 10, 33, 248, 147, 118, 12, 196, 216, 182, 115, 238, 11, 178, 97, 212, 150, 58, 157, 176, 24, 181, 46, 247, 75, 202, 175, 93, 181, 44, 35, 183, 180, 251, 100, 2, 190, 1, 227, 142, 70, 234, 194, 178, 84, 89, 155, 252, 156, 252, 73, 51, 57, 88, 147, 88, 35, 133, 5, 4, 181, 72, 250, 28, 228, 96, 26, 19, 120, 60, 184, 170, 93, 115, 129 } });
        }
    }
}
