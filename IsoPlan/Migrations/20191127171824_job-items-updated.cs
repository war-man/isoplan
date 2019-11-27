using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IsoPlan.Migrations
{
    public partial class jobitemsupdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobItem_Jobs_JobId",
                table: "JobItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobItem",
                table: "JobItem");

            migrationBuilder.RenameTable(
                name: "JobItem",
                newName: "JobItems");

            migrationBuilder.RenameIndex(
                name: "IX_JobItem_JobId",
                table: "JobItems",
                newName: "IX_JobItems_JobId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobItems",
                table: "JobItems",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 185, 199, 17, 130, 163, 51, 210, 131, 53, 17, 83, 7, 6, 153, 73, 159, 154, 199, 99, 44, 23, 1, 56, 223, 127, 230, 108, 5, 119, 129, 21, 58, 73, 152, 139, 14, 197, 15, 36, 5, 33, 163, 32, 77, 209, 71, 2, 64, 209, 133, 14, 193, 175, 143, 58, 46, 121, 198, 63, 27, 45, 185, 41, 106 }, new byte[] { 192, 228, 212, 18, 188, 179, 5, 208, 64, 143, 84, 254, 50, 73, 93, 114, 10, 164, 43, 155, 64, 161, 25, 168, 171, 82, 176, 233, 125, 229, 248, 154, 94, 83, 166, 247, 195, 5, 69, 110, 116, 227, 197, 236, 113, 16, 73, 229, 172, 162, 42, 158, 67, 45, 196, 61, 216, 149, 178, 219, 171, 35, 99, 72, 62, 248, 115, 104, 62, 156, 183, 52, 194, 120, 69, 166, 196, 25, 251, 34, 12, 173, 119, 200, 200, 127, 106, 172, 176, 164, 50, 161, 154, 153, 203, 207, 20, 194, 67, 184, 221, 171, 63, 136, 91, 135, 43, 254, 84, 11, 221, 78, 109, 29, 171, 59, 44, 239, 228, 31, 69, 58, 154, 2, 131, 72, 250, 28 } });

            migrationBuilder.AddForeignKey(
                name: "FK_JobItems_Jobs_JobId",
                table: "JobItems",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobItems_Jobs_JobId",
                table: "JobItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobItems",
                table: "JobItems");

            migrationBuilder.RenameTable(
                name: "JobItems",
                newName: "JobItem");

            migrationBuilder.RenameIndex(
                name: "IX_JobItems_JobId",
                table: "JobItem",
                newName: "IX_JobItem_JobId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobItem",
                table: "JobItem",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 174, 107, 45, 208, 126, 6, 176, 52, 185, 158, 166, 255, 189, 238, 10, 161, 248, 5, 238, 19, 198, 54, 74, 103, 83, 187, 130, 30, 51, 42, 131, 52, 18, 226, 198, 122, 182, 34, 156, 69, 120, 255, 83, 238, 85, 92, 244, 215, 203, 50, 65, 104, 128, 16, 43, 236, 170, 199, 91, 130, 233, 251, 58, 75 }, new byte[] { 198, 22, 132, 196, 205, 92, 250, 242, 49, 149, 198, 194, 4, 61, 17, 235, 27, 39, 240, 72, 211, 136, 152, 102, 192, 101, 174, 120, 214, 188, 65, 10, 28, 193, 51, 107, 118, 185, 151, 99, 120, 37, 254, 197, 8, 212, 156, 31, 207, 234, 192, 52, 91, 178, 185, 186, 4, 25, 86, 191, 35, 58, 93, 210, 3, 36, 167, 155, 50, 183, 128, 130, 180, 229, 104, 146, 112, 27, 54, 97, 181, 80, 166, 143, 48, 47, 113, 146, 74, 147, 96, 157, 60, 103, 9, 229, 43, 31, 195, 1, 9, 184, 231, 193, 239, 185, 9, 149, 148, 67, 149, 5, 49, 3, 39, 108, 78, 45, 123, 140, 240, 246, 53, 11, 158, 140, 41, 99 } });

            migrationBuilder.AddForeignKey(
                name: "FK_JobItem_Jobs_JobId",
                table: "JobItem",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
