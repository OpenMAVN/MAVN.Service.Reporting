using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.Reporting.MsSqlRepositories.Migrations
{
    public partial class AddIndicesByCampaignAndPartnerId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "partner_id",
                schema: "report",
                table: "transactions_report_2",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_transactions_report_2_campaign_id",
                schema: "report",
                table: "transactions_report_2",
                column: "campaign_id");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_report_2_partner_id",
                schema: "report",
                table: "transactions_report_2",
                column: "partner_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_transactions_report_2_campaign_id",
                schema: "report",
                table: "transactions_report_2");

            migrationBuilder.DropIndex(
                name: "IX_transactions_report_2_partner_id",
                schema: "report",
                table: "transactions_report_2");

            migrationBuilder.AlterColumn<string>(
                name: "partner_id",
                schema: "report",
                table: "transactions_report_2",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
