using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContosoUniversity.Migrations
{
    public partial class DbUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Memberships_Student_Student1ID",
                table: "Memberships");

            migrationBuilder.DropForeignKey(
                name: "FK_Memberships_Student_Student2ID",
                table: "Memberships");

            migrationBuilder.AddForeignKey(
                name: "FK_Memberships_Student_Student1ID",
                table: "Memberships",
                column: "Student1ID",
                principalTable: "Student",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Memberships_Student_Student2ID",
                table: "Memberships",
                column: "Student2ID",
                principalTable: "Student",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Memberships_Student_Student1ID",
                table: "Memberships");

            migrationBuilder.DropForeignKey(
                name: "FK_Memberships_Student_Student2ID",
                table: "Memberships");

            migrationBuilder.AddForeignKey(
                name: "FK_Memberships_Student_Student1ID",
                table: "Memberships",
                column: "Student1ID",
                principalTable: "Student",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Memberships_Student_Student2ID",
                table: "Memberships",
                column: "Student2ID",
                principalTable: "Student",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
