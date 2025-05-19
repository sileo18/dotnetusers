using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnetusers.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTrackIdFromGenres2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "track_id",
                table: "genres");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "track_id",
                table: "genres",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
