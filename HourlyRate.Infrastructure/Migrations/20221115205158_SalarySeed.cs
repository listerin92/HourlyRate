using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspStudio.Migrations
{
    public partial class SalarySeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Salaries",
                columns: new[] { "Id", "Amount", "EmployeeId", "FinancialYearId" },
                values: new object[,]
                {
                    { 1, 5000m, 1, 8 },
                    { 2, 2322m, 2, 8 },
                    { 3, 1211m, 3, 8 },
                    { 4, 855m, 4, 8 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Salaries",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Salaries",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Salaries",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Salaries",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
