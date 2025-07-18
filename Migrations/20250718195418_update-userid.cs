using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisionOfChosen_BE.Migrations
{
    /// <inheritdoc />
    public partial class updateuserid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "user_id",
                table: "user",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "user_id",
                table: "scan_detail",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "user_id",
                table: "scan",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "user_id",
                table: "drift",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "user_id",
                table: "user");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "scan_detail");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "scan");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "drift");
        }
    }
}
