using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaxCalc.Migrations
{
    public partial class AddCalculatedTaxToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalculatedTax",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnnualIncome = table.Column<double>(nullable: false),
                    PostalCode = table.Column<string>(nullable: false),
                    TransDate = table.Column<DateTime>(nullable: false),
                    CalcValue = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculatedTax", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalculatedTax");
        }
    }
}
