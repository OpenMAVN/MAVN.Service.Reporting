using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.Reporting.MsSqlRepositories.Migrations
{
    public partial class AddPartnerIdAndCurrency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "currency",
                schema: "report",
                table: "transactions_report_2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "partner_id",
                schema: "report",
                table: "transactions_report_2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "currency",
                schema: "report",
                table: "transactions_report_2");

            migrationBuilder.DropColumn(
                name: "partner_id",
                schema: "report",
                table: "transactions_report_2");
        }
    }
}
