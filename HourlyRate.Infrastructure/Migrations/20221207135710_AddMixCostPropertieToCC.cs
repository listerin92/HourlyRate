using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HourlyRate.Infrastructure.Migrations
{
    public partial class AddMixCostPropertieToCC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalMixCosts",
                table: "CostCenters",
                type: "money",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalMixCosts",
                table: "CostCenters");
        }
    }
}
