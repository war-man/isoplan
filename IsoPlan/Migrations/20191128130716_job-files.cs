using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IsoPlan.Migrations
{
    public partial class jobfiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobFile",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    Folder = table.Column<string>(nullable: true),
                    JobId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobFile_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 132, 89, 127, 115, 143, 57, 118, 216, 62, 120, 186, 105, 235, 200, 238, 194, 48, 16, 199, 177, 191, 98, 229, 8, 72, 91, 254, 63, 19, 72, 27, 51, 110, 154, 74, 224, 78, 128, 66, 158, 62, 200, 188, 147, 85, 47, 156, 138, 79, 35, 14, 40, 146, 79, 161, 240, 69, 65, 20, 173, 170, 139, 148, 208 }, new byte[] { 252, 248, 138, 192, 132, 17, 170, 126, 228, 201, 166, 111, 143, 190, 173, 87, 59, 94, 164, 10, 213, 125, 191, 55, 6, 239, 67, 108, 152, 239, 80, 202, 49, 72, 200, 126, 49, 158, 21, 48, 247, 130, 65, 220, 142, 133, 89, 119, 168, 243, 216, 113, 68, 73, 35, 229, 98, 113, 27, 197, 141, 250, 10, 136, 245, 88, 16, 21, 1, 86, 68, 132, 249, 157, 146, 181, 255, 132, 67, 227, 101, 231, 187, 247, 215, 100, 198, 2, 9, 170, 170, 100, 37, 252, 24, 36, 159, 217, 84, 152, 250, 8, 17, 146, 30, 94, 213, 151, 150, 207, 217, 121, 187, 25, 179, 210, 72, 241, 75, 24, 179, 76, 91, 233, 40, 126, 104, 157 } });

            migrationBuilder.CreateIndex(
                name: "IX_JobFile_JobId",
                table: "JobFile",
                column: "JobId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobFile");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 185, 199, 17, 130, 163, 51, 210, 131, 53, 17, 83, 7, 6, 153, 73, 159, 154, 199, 99, 44, 23, 1, 56, 223, 127, 230, 108, 5, 119, 129, 21, 58, 73, 152, 139, 14, 197, 15, 36, 5, 33, 163, 32, 77, 209, 71, 2, 64, 209, 133, 14, 193, 175, 143, 58, 46, 121, 198, 63, 27, 45, 185, 41, 106 }, new byte[] { 192, 228, 212, 18, 188, 179, 5, 208, 64, 143, 84, 254, 50, 73, 93, 114, 10, 164, 43, 155, 64, 161, 25, 168, 171, 82, 176, 233, 125, 229, 248, 154, 94, 83, 166, 247, 195, 5, 69, 110, 116, 227, 197, 236, 113, 16, 73, 229, 172, 162, 42, 158, 67, 45, 196, 61, 216, 149, 178, 219, 171, 35, 99, 72, 62, 248, 115, 104, 62, 156, 183, 52, 194, 120, 69, 166, 196, 25, 251, 34, 12, 173, 119, 200, 200, 127, 106, 172, 176, 164, 50, 161, 154, 153, 203, 207, 20, 194, 67, 184, 221, 171, 63, 136, 91, 135, 43, 254, 84, 11, 221, 78, 109, 29, 171, 59, 44, 239, 228, 31, 69, 58, 154, 2, 131, 72, 250, 28 } });
        }
    }
}
