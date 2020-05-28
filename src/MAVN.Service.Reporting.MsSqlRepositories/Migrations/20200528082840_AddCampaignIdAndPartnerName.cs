using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.Reporting.MsSqlRepositories.Migrations
{
    public partial class AddCampaignIdAndPartnerName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "campaign_id",
                schema: "report",
                table: "transactions_report_2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "partner_name",
                schema: "report",
                table: "transactions_report_2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "campaign_id",
                schema: "report",
                table: "transactions_report_2");

            migrationBuilder.DropColumn(
                name: "partner_name",
                schema: "report",
                table: "transactions_report_2");
        }
    }
}
