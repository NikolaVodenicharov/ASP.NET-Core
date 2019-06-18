using Microsoft.EntityFrameworkCore.Migrations;

namespace CameraBazaar2.Data.Migrations
{
    public partial class QuantitySyntaxError : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quiantity",
                table: "Cameras",
                newName: "Quantity");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Cameras",
                newName: "Quiantity");
        }
    }
}
