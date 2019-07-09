using Microsoft.EntityFrameworkCore.Migrations;

namespace LearningSystem.Data.Migrations
{
    public partial class StudentGrade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseUsers_Courses_CourseId",
                table: "CourseUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseUsers_AspNetUsers_UserId",
                table: "CourseUsers");

            migrationBuilder.AddColumn<int>(
                name: "StudentGrade",
                table: "CourseUsers",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseUsers_Courses_CourseId",
                table: "CourseUsers",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseUsers_AspNetUsers_UserId",
                table: "CourseUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseUsers_Courses_CourseId",
                table: "CourseUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseUsers_AspNetUsers_UserId",
                table: "CourseUsers");

            migrationBuilder.DropColumn(
                name: "StudentGrade",
                table: "CourseUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseUsers_Courses_CourseId",
                table: "CourseUsers",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseUsers_AspNetUsers_UserId",
                table: "CourseUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
