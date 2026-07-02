using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniLms.Migrations
{
    /// <inheritdoc />
    public partial class AddCourseDocumentsPhysicalTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseDocument_Courses_CourseId",
                table: "CourseDocument");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseDocument",
                table: "CourseDocument");

            migrationBuilder.RenameTable(
                name: "CourseDocument",
                newName: "CourseDocuments");

            migrationBuilder.RenameIndex(
                name: "IX_CourseDocument_CourseId",
                table: "CourseDocuments",
                newName: "IX_CourseDocuments_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseDocuments",
                table: "CourseDocuments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseDocuments_Courses_CourseId",
                table: "CourseDocuments",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseDocuments_Courses_CourseId",
                table: "CourseDocuments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseDocuments",
                table: "CourseDocuments");

            migrationBuilder.RenameTable(
                name: "CourseDocuments",
                newName: "CourseDocument");

            migrationBuilder.RenameIndex(
                name: "IX_CourseDocuments_CourseId",
                table: "CourseDocument",
                newName: "IX_CourseDocument_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseDocument",
                table: "CourseDocument",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseDocument_Courses_CourseId",
                table: "CourseDocument",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
