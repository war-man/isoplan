using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IsoPlan.Migrations
{
    public partial class jobitems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    Buy = table.Column<float>(nullable: false),
                    Sell = table.Column<float>(nullable: false),
                    Profit = table.Column<float>(nullable: false),
                    JobId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobItem_Jobs_JobId",
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
                values: new object[] { new byte[] { 174, 107, 45, 208, 126, 6, 176, 52, 185, 158, 166, 255, 189, 238, 10, 161, 248, 5, 238, 19, 198, 54, 74, 103, 83, 187, 130, 30, 51, 42, 131, 52, 18, 226, 198, 122, 182, 34, 156, 69, 120, 255, 83, 238, 85, 92, 244, 215, 203, 50, 65, 104, 128, 16, 43, 236, 170, 199, 91, 130, 233, 251, 58, 75 }, new byte[] { 198, 22, 132, 196, 205, 92, 250, 242, 49, 149, 198, 194, 4, 61, 17, 235, 27, 39, 240, 72, 211, 136, 152, 102, 192, 101, 174, 120, 214, 188, 65, 10, 28, 193, 51, 107, 118, 185, 151, 99, 120, 37, 254, 197, 8, 212, 156, 31, 207, 234, 192, 52, 91, 178, 185, 186, 4, 25, 86, 191, 35, 58, 93, 210, 3, 36, 167, 155, 50, 183, 128, 130, 180, 229, 104, 146, 112, 27, 54, 97, 181, 80, 166, 143, 48, 47, 113, 146, 74, 147, 96, 157, 60, 103, 9, 229, 43, 31, 195, 1, 9, 184, 231, 193, 239, 185, 9, 149, 148, 67, 149, 5, 49, 3, 39, 108, 78, 45, 123, 140, 240, 246, 53, 11, 158, 140, 41, 99 } });

            migrationBuilder.CreateIndex(
                name: "IX_JobItem_JobId",
                table: "JobItem",
                column: "JobId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobItem");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 168, 229, 161, 22, 201, 21, 186, 45, 220, 100, 97, 138, 246, 255, 235, 81, 116, 69, 226, 88, 91, 97, 221, 78, 140, 177, 108, 76, 155, 255, 137, 0, 88, 113, 59, 133, 222, 170, 253, 62, 197, 220, 118, 87, 171, 165, 42, 140, 34, 149, 252, 202, 240, 60, 145, 141, 4, 81, 119, 169, 22, 36, 242, 211 }, new byte[] { 228, 99, 41, 156, 64, 216, 128, 46, 151, 1, 119, 77, 171, 7, 112, 237, 7, 199, 212, 197, 124, 62, 103, 64, 180, 163, 218, 65, 90, 252, 194, 220, 135, 69, 225, 131, 61, 246, 73, 32, 240, 130, 42, 227, 203, 164, 135, 249, 42, 174, 153, 241, 10, 26, 196, 136, 5, 75, 165, 133, 191, 51, 25, 253, 210, 10, 134, 125, 152, 184, 135, 44, 96, 192, 94, 66, 10, 114, 247, 218, 115, 231, 192, 162, 22, 197, 130, 160, 100, 113, 250, 11, 139, 48, 59, 223, 203, 205, 104, 255, 83, 92, 80, 239, 214, 216, 8, 225, 165, 39, 209, 2, 12, 118, 34, 231, 186, 96, 109, 224, 90, 230, 229, 245, 23, 57, 235, 126 } });
        }
    }
}
