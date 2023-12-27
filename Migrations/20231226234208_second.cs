using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nava.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileType",
                table: "musics");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileType",
                table: "musics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
