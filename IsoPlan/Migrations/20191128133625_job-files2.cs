using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IsoPlan.Migrations
{
    public partial class jobfiles2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobFile_Jobs_JobId",
                table: "JobFile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobFile",
                table: "JobFile");

            migrationBuilder.RenameTable(
                name: "JobFile",
                newName: "JobFiles");

            migrationBuilder.RenameIndex(
                name: "IX_JobFile_JobId",
                table: "JobFiles",
                newName: "IX_JobFiles_JobId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobFiles",
                table: "JobFiles",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 105, 45, 186, 170, 70, 5, 114, 166, 171, 112, 40, 68, 120, 236, 0, 60, 53, 5, 72, 36, 127, 4, 99, 176, 117, 247, 98, 244, 243, 170, 162, 93, 100, 4, 198, 122, 196, 142, 199, 49, 108, 244, 35, 223, 157, 16, 231, 234, 3, 180, 26, 205, 184, 15, 180, 199, 160, 186, 201, 115, 18, 232, 188, 106 }, new byte[] { 226, 46, 182, 147, 193, 86, 234, 244, 45, 20, 234, 0, 141, 242, 242, 49, 249, 150, 207, 175, 119, 218, 6, 238, 206, 168, 13, 232, 108, 25, 19, 7, 21, 204, 231, 45, 255, 14, 57, 88, 81, 160, 101, 73, 174, 11, 141, 30, 53, 57, 172, 39, 208, 231, 123, 65, 3, 237, 113, 77, 9, 52, 61, 75, 121, 3, 77, 255, 166, 44, 46, 142, 128, 57, 105, 85, 70, 255, 240, 47, 150, 6, 31, 193, 224, 137, 6, 10, 237, 250, 209, 99, 101, 209, 166, 216, 252, 199, 47, 252, 22, 126, 30, 31, 145, 231, 65, 110, 7, 169, 84, 83, 48, 237, 125, 89, 169, 105, 183, 99, 80, 115, 153, 133, 97, 64, 110, 159 } });

            migrationBuilder.AddForeignKey(
                name: "FK_JobFiles_Jobs_JobId",
                table: "JobFiles",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobFiles_Jobs_JobId",
                table: "JobFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobFiles",
                table: "JobFiles");

            migrationBuilder.RenameTable(
                name: "JobFiles",
                newName: "JobFile");

            migrationBuilder.RenameIndex(
                name: "IX_JobFiles_JobId",
                table: "JobFile",
                newName: "IX_JobFile_JobId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobFile",
                table: "JobFile",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 132, 89, 127, 115, 143, 57, 118, 216, 62, 120, 186, 105, 235, 200, 238, 194, 48, 16, 199, 177, 191, 98, 229, 8, 72, 91, 254, 63, 19, 72, 27, 51, 110, 154, 74, 224, 78, 128, 66, 158, 62, 200, 188, 147, 85, 47, 156, 138, 79, 35, 14, 40, 146, 79, 161, 240, 69, 65, 20, 173, 170, 139, 148, 208 }, new byte[] { 252, 248, 138, 192, 132, 17, 170, 126, 228, 201, 166, 111, 143, 190, 173, 87, 59, 94, 164, 10, 213, 125, 191, 55, 6, 239, 67, 108, 152, 239, 80, 202, 49, 72, 200, 126, 49, 158, 21, 48, 247, 130, 65, 220, 142, 133, 89, 119, 168, 243, 216, 113, 68, 73, 35, 229, 98, 113, 27, 197, 141, 250, 10, 136, 245, 88, 16, 21, 1, 86, 68, 132, 249, 157, 146, 181, 255, 132, 67, 227, 101, 231, 187, 247, 215, 100, 198, 2, 9, 170, 170, 100, 37, 252, 24, 36, 159, 217, 84, 152, 250, 8, 17, 146, 30, 94, 213, 151, 150, 207, 217, 121, 187, 25, 179, 210, 72, 241, 75, 24, 179, 76, 91, 233, 40, 126, 104, 157 } });

            migrationBuilder.AddForeignKey(
                name: "FK_JobFile_Jobs_JobId",
                table: "JobFile",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
