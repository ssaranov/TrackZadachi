using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackZadach.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMissionStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Missions",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Missions");
        }
    }
}
