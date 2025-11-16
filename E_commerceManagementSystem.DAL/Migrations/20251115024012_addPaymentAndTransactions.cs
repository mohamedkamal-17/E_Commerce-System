using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_commerceManagementSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addPaymentAndTransactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "PaymentAmount",
            //    table: "Payments");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Payments");

            //migrationBuilder.DropColumn(
            //    name: "PaymentStatus",
            //    table: "Payments");

            //migrationBuilder.RenameColumn(
            //    name: "PaymentDate",
            //    table: "Payments",
            //    newName: "UpdatedAt");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "Payments",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "CompletedAt",
                table: "Payments",
                type: "datetime2",
                nullable: true);

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "CreatedAt",
            //    table: "Payments",
            //    type: "datetime2",
            //    nullable: false,
            //    defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            //migrationBuilder.AddColumn<string>(
            //    name: "Currency",
            //    table: "Payments",
            //    type: "nvarchar(3)",
            //    maxLength: 3,
            //    nullable: false,
            //    defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomerEmail",
                table: "Payments",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomerFirstName",
                table: "Payments",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomerLastName",
                table: "Payments",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomerPhone",
                table: "Payments",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiresAt",
                table: "Payments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GatewayName",
                table: "Payments",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GatewayOrderId",
                table: "Payments",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GatewayPaymentToken",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GatewayPaymentUrl",
                table: "Payments",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GatewayRawResponse",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastTransactionId",
                table: "Payments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastTransactionTransactionId",
                table: "Payments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Metadata",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SelectedPaymentMethod",
                table: "Payments",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);


            migrationBuilder.DropColumn(
                name: "Status",
                table: "Payments");


            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentId = table.Column<int>(type: "int", nullable: false),
                    GatewayName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GatewayTransactionId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    GatewayRawResponse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    MaskedCardNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CardBrand = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ErrorMessage = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ErrorCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Metadata = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProcessedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transactions_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_GatewayName",
                table: "Payments",
                column: "GatewayName");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_GatewayName_GatewayOrderId",
                table: "Payments",
                columns: new[] { "GatewayName", "GatewayOrderId" });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_GatewayOrderId",
                table: "Payments",
                column: "GatewayOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_LastTransactionTransactionId",
                table: "Payments",
                column: "LastTransactionTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_Status_CreatedAt",
                table: "Payments",
                columns: new[] { "Status", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_GatewayName",
                table: "Transactions",
                column: "GatewayName");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_GatewayName_GatewayTransactionId",
                table: "Transactions",
                columns: new[] { "GatewayName", "GatewayTransactionId" });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_GatewayTransactionId",
                table: "Transactions",
                column: "GatewayTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_PaymentId",
                table: "Transactions",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ProcessedAt",
                table: "Transactions",
                column: "ProcessedAt");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Transactions_LastTransactionTransactionId",
                table: "Payments",
                column: "LastTransactionTransactionId",
                principalTable: "Transactions",
                principalColumn: "TransactionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Transactions_LastTransactionTransactionId",
                table: "Payments");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Payments_GatewayName",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_GatewayName_GatewayOrderId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_GatewayOrderId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_LastTransactionTransactionId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_Status_CreatedAt",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "CompletedAt",
                table: "Payments");

            //migrationBuilder.DropColumn(
            //    name: "CreatedAt",
            //    table: "Payments");

            //migrationBuilder.DropColumn(
            //    name: "Currency",
            //    table: "Payments");

            migrationBuilder.DropColumn(
                name: "CustomerEmail",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "CustomerFirstName",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "CustomerLastName",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "CustomerPhone",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "ExpiresAt",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "GatewayName",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "GatewayOrderId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "GatewayPaymentToken",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "GatewayPaymentUrl",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "GatewayRawResponse",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "LastTransactionId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "LastTransactionTransactionId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Metadata",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "SelectedPaymentMethod",
                table: "Payments");


            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);
            //migrationBuilder.DropColumn(
            //    name: "Status",
            //    table: "Payments");

            //migrationBuilder.RenameColumn(
            //    name: "UpdatedAt",
            //    table: "Payments",
            //    newName: "PaymentDate");

            //migrationBuilder.AddColumn<double>(
            //    name: "PaymentAmount",
            //    table: "Payments",
            //    type: "float",
            //    nullable: false,
            //    defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            //migrationBuilder.AddColumn<string>(
            //    name: "PaymentStatus",
            //    table: "Payments",
            //    type: "nvarchar(max)",
            //    nullable: false,
            //    defaultValue: "");
        }
    }
}
