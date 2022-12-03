using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HourlyRate.Infrastructure.Migrations
{
    public partial class seedCostCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CostCategories",
                columns: new[] { "Id", "CompanyId", "Name" },
                values: new object[,]
                {
                    { 1, new Guid("4b609df0-6f9c-4226-1bce-08dad4a028bb"), "Water" },
                    { 2, new Guid("4b609df0-6f9c-4226-1bce-08dad4a028bb"), "Power" },
                    { 3, new Guid("4b609df0-6f9c-4226-1bce-08dad4a028bb"), "Phones" },
                    { 4, new Guid("4b609df0-6f9c-4226-1bce-08dad4a028bb"), "Other" },
                    { 5, new Guid("4b609df0-6f9c-4226-1bce-08dad4a028bb"), "Administration" },
                    { 6, new Guid("4b609df0-6f9c-4226-1bce-08dad4a028bb"), "Rent" },
                    { 7, new Guid("4b609df0-6f9c-4226-1bce-08dad4a028bb"), "Direct Repairs" },
                    { 8, new Guid("4b609df0-6f9c-4226-1bce-08dad4a028bb"), "Direct Depreciation" },
                    { 9, new Guid("4b609df0-6f9c-4226-1bce-08dad4a028bb"), "Heating" },
                    { 10, new Guid("4b609df0-6f9c-4226-1bce-08dad4a028bb"), "Taxes" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CostCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CostCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CostCategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CostCategories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CostCategories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "CostCategories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "CostCategories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "CostCategories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "CostCategories",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "CostCategories",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
