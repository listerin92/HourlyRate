using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HourlyRate.Infrastructure.Migrations
{
    public partial class changeCC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CostCenter_Departments_DepartmentId",
                table: "CostCenter");

            migrationBuilder.AddForeignKey(
                name: "FK_CostCenter_Departments_DepartmentId",
                table: "CostCenter",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CostCenter_Departments_DepartmentId",
                table: "CostCenter");

            migrationBuilder.AddForeignKey(
                name: "FK_CostCenter_Departments_DepartmentId",
                table: "CostCenter",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
