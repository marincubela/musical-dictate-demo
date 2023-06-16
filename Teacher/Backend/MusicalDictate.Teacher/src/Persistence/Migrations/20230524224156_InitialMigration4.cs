using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Sheets_SolutionId",
                table: "Assignments");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_SolutionId",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "SolutionId",
                table: "Assignments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SolutionId",
                table: "Assignments",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_SolutionId",
                table: "Assignments",
                column: "SolutionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Sheets_SolutionId",
                table: "Assignments",
                column: "SolutionId",
                principalTable: "Sheets",
                principalColumn: "Id");
        }
    }
}
