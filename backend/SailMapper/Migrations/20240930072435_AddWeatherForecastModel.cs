using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SailMapper.Migrations
{
    /// <inheritdoc />
    public partial class AddWeatherForecastModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "WeatherForecasts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "Participants",
                table: "Races",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WeatherForecasts",
                table: "WeatherForecasts",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WeatherForecasts",
                table: "WeatherForecasts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "WeatherForecasts");

            migrationBuilder.DropColumn(
                name: "Participants",
                table: "Races");
        }
    }
}
