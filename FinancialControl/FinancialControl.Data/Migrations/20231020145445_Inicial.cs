using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialControl.Data.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Value = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Revenues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Value = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Revenues", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Category", "Date", "Description", "Value" },
                values: new object[,]
                {
                    { 1, 0, new DateTime(2023, 10, 20, 11, 54, 45, 459, DateTimeKind.Local).AddTicks(2156), "Mensalidade facul", 700m },
                    { 2, 0, new DateTime(2023, 10, 20, 11, 54, 45, 459, DateTimeKind.Local).AddTicks(2614), "Internet", 70m }
                });

            migrationBuilder.InsertData(
                table: "Revenues",
                columns: new[] { "Id", "Date", "Description", "Value" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 10, 20, 11, 54, 45, 459, DateTimeKind.Local).AddTicks(2058), "Salário", 3000m },
                    { 2, new DateTime(2023, 10, 20, 11, 54, 45, 459, DateTimeKind.Local).AddTicks(2128), "Salário bônus", 3000m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Revenues");
        }
    }
}
