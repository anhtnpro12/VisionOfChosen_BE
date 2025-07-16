using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisionOfChosen_BE.Migrations
{
    /// <inheritdoc />
    public partial class updatescandetailtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drifts_ScanDetails_ScanDetailId",
                table: "Drifts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScanDetails",
                table: "ScanDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Drifts",
                table: "Drifts");

            migrationBuilder.RenameTable(
                name: "ScanDetails",
                newName: "scan_detail");

            migrationBuilder.RenameTable(
                name: "Drifts",
                newName: "drift");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "scan_detail",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "TotalResources",
                table: "scan_detail",
                newName: "total_resources");

            migrationBuilder.RenameColumn(
                name: "ScanDate",
                table: "scan_detail",
                newName: "scan_date");

            migrationBuilder.RenameColumn(
                name: "RiskLevel",
                table: "scan_detail",
                newName: "risk_level");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "scan_detail",
                newName: "file_name");

            migrationBuilder.RenameColumn(
                name: "DriftCount",
                table: "scan_detail",
                newName: "drift_count");

            migrationBuilder.RenameColumn(
                name: "ScanDetailId",
                table: "drift",
                newName: "scan_detail_id");

            migrationBuilder.RenameColumn(
                name: "RiskLevel",
                table: "drift",
                newName: "risk_level");

            migrationBuilder.RenameColumn(
                name: "ResourceType",
                table: "drift",
                newName: "resource_type");

            migrationBuilder.RenameColumn(
                name: "ResourceName",
                table: "drift",
                newName: "resource_name");

            migrationBuilder.RenameColumn(
                name: "DriftCode",
                table: "drift",
                newName: "drift_code");

            migrationBuilder.RenameColumn(
                name: "BeforeStateJson",
                table: "drift",
                newName: "before_state_json");

            migrationBuilder.RenameColumn(
                name: "AiExplanation",
                table: "drift",
                newName: "ai_explanation");

            migrationBuilder.RenameColumn(
                name: "AiAction",
                table: "drift",
                newName: "ai_action");

            migrationBuilder.RenameColumn(
                name: "AfterStateJson",
                table: "drift",
                newName: "after_state_json");

            migrationBuilder.RenameIndex(
                name: "IX_Drifts_ScanDetailId",
                table: "drift",
                newName: "IX_drift_scan_detail_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_scan_detail",
                table: "scan_detail",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_drift",
                table: "drift",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_drift_scan_detail_scan_detail_id",
                table: "drift",
                column: "scan_detail_id",
                principalTable: "scan_detail",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_drift_scan_detail_scan_detail_id",
                table: "drift");

            migrationBuilder.DropPrimaryKey(
                name: "PK_scan_detail",
                table: "scan_detail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_drift",
                table: "drift");

            migrationBuilder.RenameTable(
                name: "scan_detail",
                newName: "ScanDetails");

            migrationBuilder.RenameTable(
                name: "drift",
                newName: "Drifts");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "ScanDetails",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "total_resources",
                table: "ScanDetails",
                newName: "TotalResources");

            migrationBuilder.RenameColumn(
                name: "scan_date",
                table: "ScanDetails",
                newName: "ScanDate");

            migrationBuilder.RenameColumn(
                name: "risk_level",
                table: "ScanDetails",
                newName: "RiskLevel");

            migrationBuilder.RenameColumn(
                name: "file_name",
                table: "ScanDetails",
                newName: "FileName");

            migrationBuilder.RenameColumn(
                name: "drift_count",
                table: "ScanDetails",
                newName: "DriftCount");

            migrationBuilder.RenameColumn(
                name: "scan_detail_id",
                table: "Drifts",
                newName: "ScanDetailId");

            migrationBuilder.RenameColumn(
                name: "risk_level",
                table: "Drifts",
                newName: "RiskLevel");

            migrationBuilder.RenameColumn(
                name: "resource_type",
                table: "Drifts",
                newName: "ResourceType");

            migrationBuilder.RenameColumn(
                name: "resource_name",
                table: "Drifts",
                newName: "ResourceName");

            migrationBuilder.RenameColumn(
                name: "drift_code",
                table: "Drifts",
                newName: "DriftCode");

            migrationBuilder.RenameColumn(
                name: "before_state_json",
                table: "Drifts",
                newName: "BeforeStateJson");

            migrationBuilder.RenameColumn(
                name: "ai_explanation",
                table: "Drifts",
                newName: "AiExplanation");

            migrationBuilder.RenameColumn(
                name: "ai_action",
                table: "Drifts",
                newName: "AiAction");

            migrationBuilder.RenameColumn(
                name: "after_state_json",
                table: "Drifts",
                newName: "AfterStateJson");

            migrationBuilder.RenameIndex(
                name: "IX_drift_scan_detail_id",
                table: "Drifts",
                newName: "IX_Drifts_ScanDetailId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScanDetails",
                table: "ScanDetails",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Drifts",
                table: "Drifts",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Drifts_ScanDetails_ScanDetailId",
                table: "Drifts",
                column: "ScanDetailId",
                principalTable: "ScanDetails",
                principalColumn: "id");
        }
    }
}
