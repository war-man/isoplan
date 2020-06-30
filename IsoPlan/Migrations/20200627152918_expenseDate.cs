using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IsoPlan.Migrations
{
    public partial class expenseDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DatePaid",
                table: "Expenses",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Paid",
                table: "Expenses",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DatePaid",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "Paid",
                table: "Expenses");
        }
    }
}
