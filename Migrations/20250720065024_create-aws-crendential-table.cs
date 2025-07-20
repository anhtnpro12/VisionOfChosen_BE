using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisionOfChosen_BE.Migrations
{
    /// <inheritdoc />
    public partial class createawscrendentialtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "aws_credential",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    user_id = table.Column<string>(type: "TEXT", nullable: false),
                    created_on = table.Column<DateTime>(type: "TEXT", nullable: true),
                    created_by = table.Column<string>(type: "TEXT", nullable: true),
                    deleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    deleted_by = table.Column<string>(type: "TEXT", nullable: true),
                    deleted_on = table.Column<DateTime>(type: "TEXT", nullable: true),
                    modified_by = table.Column<string>(type: "TEXT", nullable: true),
                    modified_on = table.Column<DateTime>(type: "TEXT", nullable: true),
                    aws_access_key_id = table.Column<string>(type: "TEXT", nullable: true),
                    aws_secret_access_key = table.Column<string>(type: "TEXT", nullable: true),
                    aws_region = table.Column<string>(type: "TEXT", nullable: true),
                    json_files = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aws_credential", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "aws_credential");
        }
    }
}
