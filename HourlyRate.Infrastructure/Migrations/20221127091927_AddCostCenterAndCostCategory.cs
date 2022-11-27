using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HourlyRate.Infrastructure.Migrations
{
    public partial class AddCostCenterAndCostCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CostCenterId",
                table: "Expenses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CostCenter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostCenter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CostCenter_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CostCategories",
                columns: new[] { "Id", "CompanyId", "Name" },
                values: new object[,]
                {
                    { 1, new Guid("457fc37b-b204-4019-9e5d-08dacf799bb2"), "FloorSpace m2" },
                    { 2, new Guid("457fc37b-b204-4019-9e5d-08dacf799bb2"), "Power Consumption kWh" },
                    { 3, new Guid("457fc37b-b204-4019-9e5d-08dacf799bb2"), "Heating" },
                    { 4, new Guid("457fc37b-b204-4019-9e5d-08dacf799bb2"), "Cooling" },
                    { 5, new Guid("457fc37b-b204-4019-9e5d-08dacf799bb2"), "General Taxes" },
                    { 6, new Guid("457fc37b-b204-4019-9e5d-08dacf799bb2"), "Direct Repairs" },
                    { 7, new Guid("457fc37b-b204-4019-9e5d-08dacf799bb2"), "Available hours" },
                    { 8, new Guid("457fc37b-b204-4019-9e5d-08dacf799bb2"), "Salable hours" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_CostCenterId",
                table: "Expenses",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_CostCenter_CompanyId",
                table: "CostCenter",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_CostCenter_CostCenterId",
                table: "Expenses",
                column: "CostCenterId",
                principalTable: "CostCenter",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_CostCenter_CostCenterId",
                table: "Expenses");

            migrationBuilder.DropTable(
                name: "CostCenter");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_CostCenterId",
                table: "Expenses");

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

            migrationBuilder.DropColumn(
                name: "CostCenterId",
                table: "Expenses");
        }
    }
}
