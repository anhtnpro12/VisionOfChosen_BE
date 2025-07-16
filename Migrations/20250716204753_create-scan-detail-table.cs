using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisionOfChosen_BE.Migrations
{
    /// <inheritdoc />
    public partial class createscandetailtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScanDetails",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    created_on = table.Column<DateTime>(type: "TEXT", nullable: true),
                    created_by = table.Column<string>(type: "TEXT", nullable: true),
                    deleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    deleted_by = table.Column<string>(type: "TEXT", nullable: true),
                    deleted_on = table.Column<DateTime>(type: "TEXT", nullable: true),
                    modified_by = table.Column<string>(type: "TEXT", nullable: true),
                    modified_on = table.Column<DateTime>(type: "TEXT", nullable: true),
                    FileName = table.Column<string>(type: "TEXT", nullable: true),
                    ScanDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: true),
                    TotalResources = table.Column<int>(type: "INTEGER", nullable: false),
                    DriftCount = table.Column<int>(type: "INTEGER", nullable: false),
                    RiskLevel = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScanDetails", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Drifts",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    created_on = table.Column<DateTime>(type: "TEXT", nullable: true),
                    created_by = table.Column<string>(type: "TEXT", nullable: true),
                    deleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    deleted_by = table.Column<string>(type: "TEXT", nullable: true),
                    deleted_on = table.Column<DateTime>(type: "TEXT", nullable: true),
                    modified_by = table.Column<string>(type: "TEXT", nullable: true),
                    modified_on = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DriftCode = table.Column<string>(type: "TEXT", nullable: true),
                    ResourceType = table.Column<string>(type: "TEXT", nullable: true),
                    ResourceName = table.Column<string>(type: "TEXT", nullable: true),
                    RiskLevel = table.Column<string>(type: "TEXT", nullable: true),
                    BeforeStateJson = table.Column<string>(type: "TEXT", nullable: true),
                    AfterStateJson = table.Column<string>(type: "TEXT", nullable: true),
                    AiExplanation = table.Column<string>(type: "TEXT", nullable: true),
                    AiAction = table.Column<string>(type: "TEXT", nullable: true),
                    ScanDetailId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drifts", x => x.id);
                    table.ForeignKey(
                        name: "FK_Drifts_ScanDetails_ScanDetailId",
                        column: x => x.ScanDetailId,
                        principalTable: "ScanDetails",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Drifts_ScanDetailId",
                table: "Drifts",
                column: "ScanDetailId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Drifts");

            migrationBuilder.DropTable(
                name: "ScanDetails");
        }
    }
}
