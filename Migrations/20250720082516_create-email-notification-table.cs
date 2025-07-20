using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisionOfChosen_BE.Migrations
{
    /// <inheritdoc />
    public partial class createemailnotificationtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "email_notification",
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
                    email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_email_notification", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "email_notification");
        }
    }
}
