using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspStudio.Migrations
{
    public partial class SeedDepartmentsYears : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Salaries",
                type: "money",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Prepress" },
                    { 2, "Press" },
                    { 3, "WebPress" },
                    { 4, "ManualLabor" },
                    { 5, "Cutters" },
                    { 6, "Stitchers" },
                    { 7, "Binders" },
                    { 8, "HardCover" },
                    { 9, "FrontCutter" },
                    { 10, "RotaryCutter" }
                });

            migrationBuilder.InsertData(
                table: "FinancialYears",
                columns: new[] { "Id", "Year" },
                values: new object[,]
                {
                    { 1, 2015 },
                    { 2, 2016 },
                    { 3, 2017 },
                    { 4, 2018 },
                    { 5, 2019 },
                    { 6, 2020 },
                    { 7, 2021 },
                    { 8, 2022 },
                    { 9, 2023 },
                    { 10, 2024 },
                    { 11, 2025 },
                    { 12, 2026 },
                    { 13, 2027 },
                    { 14, 2028 },
                    { 15, 2029 },
                    { 16, 2030 },
                    { 17, 2031 },
                    { 18, 2032 },
                    { 19, 2033 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "FinancialYears",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FinancialYears",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FinancialYears",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "FinancialYears",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "FinancialYears",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "FinancialYears",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "FinancialYears",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "FinancialYears",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "FinancialYears",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "FinancialYears",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "FinancialYears",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "FinancialYears",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "FinancialYears",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "FinancialYears",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "FinancialYears",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "FinancialYears",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "FinancialYears",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "FinancialYears",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "FinancialYears",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Salaries",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money",
                oldPrecision: 18,
                oldScale: 2);
        }
    }
}
