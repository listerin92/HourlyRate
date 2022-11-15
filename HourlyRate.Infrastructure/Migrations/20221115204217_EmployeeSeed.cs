using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspStudio.Migrations
{
    public partial class EmployeeSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "DepartmentId", "FirstName", "IsEmployee", "JobTitle", "LastName" },
                values: new object[,]
                {
                    { 1, 1, "Ivan", true, "asdf", "Ivanov" },
                    { 2, 2, "Petar", true, "asdf", "Petrov" },
                    { 3, 1, "Stefan", true, "bbb", "Todorov" },
                    { 4, 1, "Georgi", true, "asdf", "Antonov" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
