using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HourlyRate.Infrastructure.Migrations
{
    public partial class AddDescriptionToExpenses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Expenses",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Expenses");
        }
    }
}
