using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SailMapper.Migrations
{
    /// <inheritdoc />
    public partial class SingleCourseInRace : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Races_RaceId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_RaceId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "RaceId",
                table: "Courses");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "RaceId",
                table: "Courses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_RaceId",
                table: "Courses",
                column: "RaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Races_RaceId",
                table: "Courses",
                column: "RaceId",
                principalTable: "Races",
                principalColumn: "Id");
        }
    }
}
