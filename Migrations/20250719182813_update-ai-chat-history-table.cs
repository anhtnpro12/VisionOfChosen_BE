using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisionOfChosen_BE.Migrations
{
    /// <inheritdoc />
    public partial class updateaichathistorytable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "json_files",
                table: "ai_chat_history",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "json_files",
                table: "ai_chat_history");
        }
    }
}
