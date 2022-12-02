using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HourlyRate.Infrastructure.Migrations
{
    public partial class AddAllPropertiesToCostCenterModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DirectAllocatedStuff",
                table: "CostCenter",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "DirectDepreciationCost",
                table: "CostCenter",
                type: "money",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DirectElectricityCost",
                table: "CostCenter",
                type: "money",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DirectGeneraConsumablesCost",
                table: "CostCenter",
                type: "money",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DirectRepairCost",
                table: "CostCenter",
                type: "money",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DirectWagesCost",
                table: "CostCenter",
                type: "money",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "IndirectAdministrationWagesCost",
                table: "CostCenter",
                type: "money",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "IndirectMaintenanceWagesCost",
                table: "CostCenter",
                type: "money",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "IndirectOtherCost",
                table: "CostCenter",
                type: "money",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "IndirectPhonesCost",
                table: "CostCenter",
                type: "money",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "IndirectTaxes",
                table: "CostCenter",
                type: "money",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "IndirectTotalCosts",
                table: "CostCenter",
                type: "money",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "IndirectWaterCost",
                table: "CostCenter",
                type: "money",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MachinesPerHour",
                table: "CostCenter",
                type: "money",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MachinesPerMonth",
                table: "CostCenter",
                type: "money",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OverheadsPerHour",
                table: "CostCenter",
                type: "money",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OverheadsPerMonth",
                table: "CostCenter",
                type: "money",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RentCost",
                table: "CostCenter",
                type: "money",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalCosts",
                table: "CostCenter",
                type: "money",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalDirectCosts",
                table: "CostCenter",
                type: "money",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalIndex",
                table: "CostCenter",
                type: "money",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "WagesPerHour",
                table: "CostCenter",
                type: "money",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "WagesPerMonth",
                table: "CostCenter",
                type: "money",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "WaterTotalIndex",
                table: "CostCenter",
                type: "money",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DirectAllocatedStuff",
                table: "CostCenter");

            migrationBuilder.DropColumn(
                name: "DirectDepreciationCost",
                table: "CostCenter");

            migrationBuilder.DropColumn(
                name: "DirectElectricityCost",
                table: "CostCenter");

            migrationBuilder.DropColumn(
                name: "DirectGeneraConsumablesCost",
                table: "CostCenter");

            migrationBuilder.DropColumn(
                name: "DirectRepairCost",
                table: "CostCenter");

            migrationBuilder.DropColumn(
                name: "DirectWagesCost",
                table: "CostCenter");

            migrationBuilder.DropColumn(
                name: "IndirectAdministrationWagesCost",
                table: "CostCenter");

            migrationBuilder.DropColumn(
                name: "IndirectMaintenanceWagesCost",
                table: "CostCenter");

            migrationBuilder.DropColumn(
                name: "IndirectOtherCost",
                table: "CostCenter");

            migrationBuilder.DropColumn(
                name: "IndirectPhonesCost",
                table: "CostCenter");

            migrationBuilder.DropColumn(
                name: "IndirectTaxes",
                table: "CostCenter");

            migrationBuilder.DropColumn(
                name: "IndirectTotalCosts",
                table: "CostCenter");

            migrationBuilder.DropColumn(
                name: "IndirectWaterCost",
                table: "CostCenter");

            migrationBuilder.DropColumn(
                name: "MachinesPerHour",
                table: "CostCenter");

            migrationBuilder.DropColumn(
                name: "MachinesPerMonth",
                table: "CostCenter");

            migrationBuilder.DropColumn(
                name: "OverheadsPerHour",
                table: "CostCenter");

            migrationBuilder.DropColumn(
                name: "OverheadsPerMonth",
                table: "CostCenter");

            migrationBuilder.DropColumn(
                name: "RentCost",
                table: "CostCenter");

            migrationBuilder.DropColumn(
                name: "TotalCosts",
                table: "CostCenter");

            migrationBuilder.DropColumn(
                name: "TotalDirectCosts",
                table: "CostCenter");

            migrationBuilder.DropColumn(
                name: "TotalIndex",
                table: "CostCenter");

            migrationBuilder.DropColumn(
                name: "WagesPerHour",
                table: "CostCenter");

            migrationBuilder.DropColumn(
                name: "WagesPerMonth",
                table: "CostCenter");

            migrationBuilder.DropColumn(
                name: "WaterTotalIndex",
                table: "CostCenter");
        }
    }
}
