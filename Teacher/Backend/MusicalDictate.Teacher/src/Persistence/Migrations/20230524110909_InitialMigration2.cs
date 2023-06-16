using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Sheets_SolutionId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Teachers_TeacherId",
                table: "Exercises");

            migrationBuilder.AlterColumn<string>(
                name: "TeacherId",
                table: "Exercises",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SolutionId",
                table: "Assignments",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Sheets_SolutionId",
                table: "Assignments",
                column: "SolutionId",
                principalTable: "Sheets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Teachers_TeacherId",
                table: "Exercises",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Sheets_SolutionId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Teachers_TeacherId",
                table: "Exercises");

            migrationBuilder.AlterColumn<string>(
                name: "TeacherId",
                table: "Exercises",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "SolutionId",
                table: "Assignments",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Sheets_SolutionId",
                table: "Assignments",
                column: "SolutionId",
                principalTable: "Sheets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Teachers_TeacherId",
                table: "Exercises",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id");
        }
    }
}
