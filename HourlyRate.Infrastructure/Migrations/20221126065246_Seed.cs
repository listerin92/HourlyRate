using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HourlyRate.Infrastructure.Migrations
{
    public partial class Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "CompanyId", "Name" },
                values: new object[,]
                {
                    { 1, new Guid("457fc37b-b204-4019-9e5d-08dacf799bb2"), "Prepress" },
                    { 2, new Guid("457fc37b-b204-4019-9e5d-08dacf799bb2"), "Press" },
                    { 3, new Guid("457fc37b-b204-4019-9e5d-08dacf799bb2"), "WebPress" },
                    { 4, new Guid("457fc37b-b204-4019-9e5d-08dacf799bb2"), "ManualLabor" },
                    { 5, new Guid("457fc37b-b204-4019-9e5d-08dacf799bb2"), "Cutters" },
                    { 6, new Guid("457fc37b-b204-4019-9e5d-08dacf799bb2"), "Stitchers" },
                    { 7, new Guid("457fc37b-b204-4019-9e5d-08dacf799bb2"), "Binders" },
                    { 8, new Guid("457fc37b-b204-4019-9e5d-08dacf799bb2"), "HardCover" },
                    { 9, new Guid("457fc37b-b204-4019-9e5d-08dacf799bb2"), "FrontCutter" },
                    { 10, new Guid("457fc37b-b204-4019-9e5d-08dacf799bb2"), "RotaryCutter" }
                });

            migrationBuilder.InsertData(
                table: "FinancialYears",
                columns: new[] { "Id", "IsActive", "Year" },
                values: new object[,]
                {
                    { 1, false, 2015 },
                    { 2, false, 2016 },
                    { 3, false, 2017 },
                    { 4, false, 2018 },
                    { 5, false, 2019 },
                    { 6, false, 2020 },
                    { 7, false, 2021 },
                    { 8, false, 2022 },
                    { 9, false, 2023 },
                    { 10, false, 2024 },
                    { 11, false, 2025 },
                    { 12, false, 2026 },
                    { 13, false, 2027 },
                    { 14, false, 2028 },
                    { 15, false, 2029 },
                    { 16, false, 2030 },
                    { 17, false, 2031 },
                    { 18, false, 2032 },
                    { 19, false, 2033 }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DepartmentId", "FirstName", "ImageUrl", "IsEmployee", "JobTitle", "LastName" },
                values: new object[,]
                {
                    { 1, new Guid("457fc37b-b204-4019-9e5d-08dacf799bb2"), 1, "Ivan", "https://www.loudegg.com/wp-content/uploads/2020/10/Mickey-Mouse.jpg", true, "asdf", "Ivanov" },
                    { 2, new Guid("457fc37b-b204-4019-9e5d-08dacf799bb2"), 2, "Petar", "https://www.loudegg.com/wp-content/uploads/2020/10/Bugs-Bunny.jpg", true, "asdf", "Petrov" },
                    { 3, new Guid("457fc37b-b204-4019-9e5d-08dacf799bb2"), 1, "Stefan", "https://www.loudegg.com/wp-content/uploads/2020/10/Fred-Flintstone.jpg", true, "bbb", "Todorov" },
                    { 4, new Guid("457fc37b-b204-4019-9e5d-08dacf799bb2"), 1, "Georgi", "https://www.loudegg.com/wp-content/uploads/2020/10/SpongeBob-SqaurePants.jpg", true, "asdf", "Antonov" }
                });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Amount", "CompanyId", "ConsumableId", "CostCategoryId", "EmployeeId", "FinancialYearId" },
                values: new object[,]
                {
                    { 1, 5000m, new Guid("457fc37b-b204-4019-9e5d-08dacf799bb2"), null, null, 1, 8 },
                    { 2, 2322m, new Guid("457fc37b-b204-4019-9e5d-08dacf799bb2"), null, null, 2, 8 },
                    { 3, 1211m, new Guid("457fc37b-b204-4019-9e5d-08dacf799bb2"), null, null, 3, 8 },
                    { 4, 855m, new Guid("457fc37b-b204-4019-9e5d-08dacf799bb2"), null, null, 4, 8 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 4);

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

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "FinancialYears",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
