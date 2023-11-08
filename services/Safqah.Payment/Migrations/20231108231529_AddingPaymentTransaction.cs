using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Safqah.Payment.Migrations
{
    public partial class AddingPaymentTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<string>(nullable: true),
                    PaymentStatus = table.Column<int>(nullable: false),
                    PaymentSorcue = table.Column<int>(nullable: false),
                    PaymentCardType = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTransactions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentTransactions");
        }
    }
}
