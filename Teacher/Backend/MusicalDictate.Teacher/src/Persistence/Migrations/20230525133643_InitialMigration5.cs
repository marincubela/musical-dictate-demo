using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSolution_Assignments_AssignmentId",
                table: "StudentSolution");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSolution_Result_ResultId",
                table: "StudentSolution");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSolution_Sheets_SolutionId",
                table: "StudentSolution");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSolution_Students_StudentId",
                table: "StudentSolution");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSolution_Teachers_TeacherId",
                table: "StudentSolution");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentSolution",
                table: "StudentSolution");

            migrationBuilder.RenameTable(
                name: "StudentSolution",
                newName: "StudentSolutions");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSolution_TeacherId",
                table: "StudentSolutions",
                newName: "IX_StudentSolutions_TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSolution_StudentId",
                table: "StudentSolutions",
                newName: "IX_StudentSolutions_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSolution_SolutionId",
                table: "StudentSolutions",
                newName: "IX_StudentSolutions_SolutionId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSolution_ResultId",
                table: "StudentSolutions",
                newName: "IX_StudentSolutions_ResultId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSolution_AssignmentId",
                table: "StudentSolutions",
                newName: "IX_StudentSolutions_AssignmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentSolutions",
                table: "StudentSolutions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSolutions_Assignments_AssignmentId",
                table: "StudentSolutions",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSolutions_Result_ResultId",
                table: "StudentSolutions",
                column: "ResultId",
                principalTable: "Result",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSolutions_Sheets_SolutionId",
                table: "StudentSolutions",
                column: "SolutionId",
                principalTable: "Sheets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSolutions_Students_StudentId",
                table: "StudentSolutions",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSolutions_Teachers_TeacherId",
                table: "StudentSolutions",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSolutions_Assignments_AssignmentId",
                table: "StudentSolutions");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSolutions_Result_ResultId",
                table: "StudentSolutions");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSolutions_Sheets_SolutionId",
                table: "StudentSolutions");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSolutions_Students_StudentId",
                table: "StudentSolutions");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSolutions_Teachers_TeacherId",
                table: "StudentSolutions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentSolutions",
                table: "StudentSolutions");

            migrationBuilder.RenameTable(
                name: "StudentSolutions",
                newName: "StudentSolution");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSolutions_TeacherId",
                table: "StudentSolution",
                newName: "IX_StudentSolution_TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSolutions_StudentId",
                table: "StudentSolution",
                newName: "IX_StudentSolution_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSolutions_SolutionId",
                table: "StudentSolution",
                newName: "IX_StudentSolution_SolutionId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSolutions_ResultId",
                table: "StudentSolution",
                newName: "IX_StudentSolution_ResultId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSolutions_AssignmentId",
                table: "StudentSolution",
                newName: "IX_StudentSolution_AssignmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentSolution",
                table: "StudentSolution",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSolution_Assignments_AssignmentId",
                table: "StudentSolution",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSolution_Result_ResultId",
                table: "StudentSolution",
                column: "ResultId",
                principalTable: "Result",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSolution_Sheets_SolutionId",
                table: "StudentSolution",
                column: "SolutionId",
                principalTable: "Sheets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSolution_Students_StudentId",
                table: "StudentSolution",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSolution_Teachers_TeacherId",
                table: "StudentSolution",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id");
        }
    }
}
