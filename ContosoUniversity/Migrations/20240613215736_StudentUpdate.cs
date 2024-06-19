using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContosoUniversity.Migrations
{
    public partial class StudentUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Memberships_Student_StudentID",
                table: "Memberships");

            migrationBuilder.DropIndex(
                name: "IX_Memberships_StudentID",
                table: "Memberships");

            migrationBuilder.DropColumn(
                name: "StudentID",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "StudentID",
                table: "Memberships");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentID",
                table: "Student",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StudentID",
                table: "Memberships",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_StudentID",
                table: "Memberships",
                column: "StudentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Memberships_Student_StudentID",
                table: "Memberships",
                column: "StudentID",
                principalTable: "Student",
                principalColumn: "ID");
        }
    }
}
