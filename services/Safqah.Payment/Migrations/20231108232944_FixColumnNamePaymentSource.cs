using Microsoft.EntityFrameworkCore.Migrations;

namespace Safqah.Payment.Migrations
{
    public partial class FixColumnNamePaymentSource : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentSorcue",
                table: "PaymentTransactions");

            migrationBuilder.AddColumn<int>(
                name: "PaymentSource",
                table: "PaymentTransactions",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentSource",
                table: "PaymentTransactions");

            migrationBuilder.AddColumn<int>(
                name: "PaymentSorcue",
                table: "PaymentTransactions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
