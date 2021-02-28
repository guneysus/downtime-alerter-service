using Microsoft.EntityFrameworkCore.Migrations;

namespace DowntimeAlerterWeb.Migrations.DowntimeAlerterData
{
    public partial class StatusMonitorForeignkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_StatusLogs_MonitorId",
                table: "StatusLogs",
                column: "MonitorId");

            migrationBuilder.AddForeignKey(
                name: "FK_StatusLogs_Monitors_MonitorId",
                table: "StatusLogs",
                column: "MonitorId",
                principalTable: "Monitors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StatusLogs_Monitors_MonitorId",
                table: "StatusLogs");

            migrationBuilder.DropIndex(
                name: "IX_StatusLogs_MonitorId",
                table: "StatusLogs");
        }
    }
}
