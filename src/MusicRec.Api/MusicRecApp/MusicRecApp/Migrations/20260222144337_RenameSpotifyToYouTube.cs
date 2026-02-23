using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicRecApp.Migrations
{
    /// <inheritdoc />
    public partial class RenameSpotifyToYouTube : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SpotifyId",
                table: "Favorites",
                newName: "YoutubeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "YoutubeId",
                table: "Favorites",
                newName: "SpotifyId");
        }
    }
}
