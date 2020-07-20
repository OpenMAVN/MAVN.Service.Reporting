using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.Reporting.MsSqlRepositories.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "report");

            migrationBuilder.CreateTable(
                name: "transactions_report_2",
                schema: "report",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    timestamp = table.Column<DateTime>(nullable: false),
                    amount = table.Column<string>(nullable: false),
                    transaction_type = table.Column<string>(nullable: true),
                    status = table.Column<string>(nullable: true),
                    vertical = table.Column<string>(nullable: true),
                    transaction_category = table.Column<string>(nullable: true),
                    campaign_name = table.Column<string>(nullable: true),
                    info = table.Column<string>(nullable: true),
                    sender_name = table.Column<string>(nullable: true),
                    sender_email = table.Column<string>(nullable: true),
                    receiver_name = table.Column<string>(nullable: true),
                    receiver_email = table.Column<string>(nullable: true),
                    location_info = table.Column<string>(nullable: true),
                    location_ext_id = table.Column<string>(nullable: true),
                    location_integration_code = table.Column<string>(nullable: true),
                    partner_id = table.Column<string>(nullable: true),
                    currency = table.Column<string>(nullable: true),
                    campaign_id = table.Column<Guid>(nullable: true),
                    partner_name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transactions_report_2", x => x.id);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_transactions_report_2_timestamp",
                schema: "report",
                table: "transactions_report_2",
                column: "timestamp");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "transactions_report_2",
                schema: "report");
        }
    }
}
