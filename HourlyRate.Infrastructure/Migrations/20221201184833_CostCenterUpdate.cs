using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HourlyRate.Infrastructure.Migrations
{
    public partial class CostCenterUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AnnualChargeableHours",
                table: "CostCenter",
                type: "money",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AnnualHours",
                table: "CostCenter",
                type: "money",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AvgPowerConsumptionKwh",
                table: "CostCenter",
                type: "money",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "CostCenter",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "FloorSpace",
                table: "CostCenter",
                type: "money",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsingWater",
                table: "CostCenter",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_CostCenter_DepartmentId",
                table: "CostCenter",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CostCenter_Departments_DepartmentId",
                table: "CostCenter",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CostCenter_Departments_DepartmentId",
                table: "CostCenter");

            migrationBuilder.DropIndex(
                name: "IX_CostCenter_DepartmentId",
                table: "CostCenter");

            migrationBuilder.DropColumn(
                name: "AnnualChargeableHours",
                table: "CostCenter");

            migrationBuilder.DropColumn(
                name: "AnnualHours",
                table: "CostCenter");

            migrationBuilder.DropColumn(
                name: "AvgPowerConsumptionKwh",
                table: "CostCenter");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "CostCenter");

            migrationBuilder.DropColumn(
                name: "FloorSpace",
                table: "CostCenter");

            migrationBuilder.DropColumn(
                name: "IsUsingWater",
                table: "CostCenter");
        }
    }
}
