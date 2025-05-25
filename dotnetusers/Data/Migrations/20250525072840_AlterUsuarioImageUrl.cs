using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnetusers.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlterUsuarioImageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "image_url",
                table: "usuarios",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "image_url",
                table: "usuarios");
        }
    }
}
