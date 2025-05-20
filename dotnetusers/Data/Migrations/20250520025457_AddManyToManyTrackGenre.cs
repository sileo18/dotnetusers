using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnetusers.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddManyToManyTrackGenre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tracks_genres_genre_id",
                table: "tracks");

            migrationBuilder.DropIndex(
                name: "IX_tracks_genre_id",
                table: "tracks");

            migrationBuilder.DropColumn(
                name: "genre_id",
                table: "tracks");

            migrationBuilder.CreateTable(
                name: "track_genres",
                columns: table => new
                {
                    TrackId = table.Column<int>(type: "integer", nullable: false),
                    GenreId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_track_genres", x => new { x.TrackId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_track_genres_genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "genres",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_track_genres_tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "tracks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_track_genres_GenreId",
                table: "track_genres",
                column: "GenreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "track_genres");

            migrationBuilder.AddColumn<int>(
                name: "genre_id",
                table: "tracks",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tracks_genre_id",
                table: "tracks",
                column: "genre_id");

            migrationBuilder.AddForeignKey(
                name: "FK_tracks_genres_genre_id",
                table: "tracks",
                column: "genre_id",
                principalTable: "genres",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
