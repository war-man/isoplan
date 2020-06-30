using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IsoPlan.Migrations
{
    public partial class facturespay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "TotalPaid",
                table: "Jobs",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTime>(
                name: "DatePaid",
                table: "Factures",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPaid",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "DatePaid",
                table: "Factures");
        }
    }
}
