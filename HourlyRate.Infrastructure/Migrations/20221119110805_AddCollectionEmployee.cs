using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HourlyRate.Infrastructure.Migrations
{
    public partial class AddCollectionEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExpensesId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ExpensesId",
                table: "Employees",
                column: "ExpensesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Expenses_ExpensesId",
                table: "Employees",
                column: "ExpensesId",
                principalTable: "Expenses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Expenses_ExpensesId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ExpensesId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ExpensesId",
                table: "Employees");
        }
    }
}
