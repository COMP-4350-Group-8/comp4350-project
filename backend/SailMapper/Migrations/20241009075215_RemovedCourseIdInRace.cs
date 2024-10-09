using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SailMapper.Migrations
{
    /// <inheritdoc />
    public partial class RemovedCourseIdInRace : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Races_Courses_CourseId",
                table: "Races");

            migrationBuilder.DropIndex(
                name: "IX_Races_CourseId",
                table: "Races");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Races");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Races",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Races_CourseId",
                table: "Races",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Races_Courses_CourseId",
                table: "Races",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}