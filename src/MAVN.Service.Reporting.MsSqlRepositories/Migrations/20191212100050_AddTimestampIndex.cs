using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.Reporting.MsSqlRepositories.Migrations
{
    public partial class AddTimestampIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_transactions_report_id",
                schema: "report",
                table: "transactions_report");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_report_timestamp",
                schema: "report",
                table: "transactions_report",
                column: "timestamp");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_transactions_report_timestamp",
                schema: "report",
                table: "transactions_report");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_report_id",
                schema: "report",
                table: "transactions_report",
                column: "id");
        }
    }
}
