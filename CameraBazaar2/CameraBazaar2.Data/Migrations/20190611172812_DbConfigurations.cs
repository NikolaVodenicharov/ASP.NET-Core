using Microsoft.EntityFrameworkCore.Migrations;

namespace CameraBazaar2.Data.Migrations
{
    public partial class DbConfigurations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Camera_AspNetUsers_UserId",
                table: "Camera");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Camera",
                table: "Camera");

            migrationBuilder.RenameTable(
                name: "Camera",
                newName: "Cameras");

            migrationBuilder.RenameIndex(
                name: "IX_Camera_UserId",
                table: "Cameras",
                newName: "IX_Cameras_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cameras",
                table: "Cameras",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cameras_AspNetUsers_UserId",
                table: "Cameras",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cameras_AspNetUsers_UserId",
                table: "Cameras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cameras",
                table: "Cameras");

            migrationBuilder.RenameTable(
                name: "Cameras",
                newName: "Camera");

            migrationBuilder.RenameIndex(
                name: "IX_Cameras_UserId",
                table: "Camera",
                newName: "IX_Camera_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Camera",
                table: "Camera",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Camera_AspNetUsers_UserId",
                table: "Camera",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
