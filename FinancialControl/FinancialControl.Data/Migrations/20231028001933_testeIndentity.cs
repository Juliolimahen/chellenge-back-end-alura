using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialControl.Data.Migrations
{
    public partial class testeIndentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2023, 10, 27, 21, 19, 32, 680, DateTimeKind.Local).AddTicks(5911));

            migrationBuilder.UpdateData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2023, 10, 27, 21, 19, 32, 680, DateTimeKind.Local).AddTicks(5927));

            migrationBuilder.UpdateData(
                table: "Revenues",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2023, 10, 27, 21, 19, 32, 680, DateTimeKind.Local).AddTicks(5850));

            migrationBuilder.UpdateData(
                table: "Revenues",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2023, 10, 27, 21, 19, 32, 680, DateTimeKind.Local).AddTicks(5892));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2023, 10, 26, 21, 17, 59, 371, DateTimeKind.Local).AddTicks(9191));

            migrationBuilder.UpdateData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2023, 10, 26, 21, 17, 59, 371, DateTimeKind.Local).AddTicks(9223));

            migrationBuilder.UpdateData(
                table: "Revenues",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2023, 10, 26, 21, 17, 59, 371, DateTimeKind.Local).AddTicks(9102));

            migrationBuilder.UpdateData(
                table: "Revenues",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2023, 10, 26, 21, 17, 59, 371, DateTimeKind.Local).AddTicks(9161));
        }
    }
}
