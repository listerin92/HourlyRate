using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HourlyRate.Infrastructure.Migrations
{
    public partial class addImageUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Employees",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://www.loudegg.com/wp-content/uploads/2020/10/Mickey-Mouse.jpg");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://www.loudegg.com/wp-content/uploads/2020/10/Bugs-Bunny.jpg");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "https://www.loudegg.com/wp-content/uploads/2020/10/Fred-Flintstone.jpg");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "https://www.loudegg.com/wp-content/uploads/2020/10/SpongeBob-SqaurePants.jpg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Employees");
        }
    }
}
