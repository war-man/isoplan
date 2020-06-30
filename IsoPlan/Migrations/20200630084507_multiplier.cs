using Microsoft.EntityFrameworkCore.Migrations;

namespace IsoPlan.Migrations
{
    public partial class multiplier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Multiplier",
                table: "Schedules",
                nullable: false,
                defaultValue: 1.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Multiplier",
                table: "Schedules");
        }
    }
}
