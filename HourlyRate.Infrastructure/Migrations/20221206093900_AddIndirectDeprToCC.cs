using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HourlyRate.Infrastructure.Migrations
{
    public partial class AddIndirectDeprToCC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "IndirectDepreciationCost",
                table: "CostCenters",
                type: "money",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "CostCategories",
                columns: new[] { "Id", "CompanyId", "Name" },
                values: new object[] { 11, new Guid("4b609df0-6f9c-4226-1bce-08dad4a028bb"), "Indirect Depreciation" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CostCategories",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DropColumn(
                name: "IndirectDepreciationCost",
                table: "CostCenters");
        }
    }
}
