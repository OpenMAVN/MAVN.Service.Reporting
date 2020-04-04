using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.Reporting.MsSqlRepositories.Migrations
{
    public partial class TransactionsReportEntity_v0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "report");

            migrationBuilder.CreateTable(
                name: "transactions_report",
                schema: "report",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    sender_customer_name = table.Column<string>(nullable: true),
                    sender_customer_email = table.Column<string>(nullable: true),
                    sender_customer_wallet = table.Column<string>(nullable: true),
                    inbound_wallet_address = table.Column<string>(nullable: true),
                    timestamp = table.Column<DateTime>(nullable: true),
                    transaction_type = table.Column<string>(nullable: true),
                    action_rule_name = table.Column<string>(nullable: true),
                    receiver_customer_name = table.Column<string>(nullable: true),
                    receiver_customer_email = table.Column<string>(nullable: true),
                    receiver_customer_wallet = table.Column<string>(nullable: true),
                    outbound_wallet_address = table.Column<string>(nullable: true),
                    amount = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transactions_report", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_transactions_report_id",
                schema: "report",
                table: "transactions_report",
                column: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "transactions_report",
                schema: "report");
        }
    }
}
