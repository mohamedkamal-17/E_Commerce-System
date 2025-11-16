using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_commerceManagementSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addIdToCartItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishLists_Products_ProductID",
                table: "WishLists");

            migrationBuilder.DropIndex(
                name: "IX_WishLists_ProductID",
                table: "WishLists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartItems",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "ProductID",
                table: "WishLists");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "WishLists",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "PaymentStatus",
                table: "Payments",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "PaymentDate",
                table: "Payments",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "PaymentAmount",
                table: "Payments",
                newName: "TotalAmount");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "WishLists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Payments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PaymentIntentId",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentIntentId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartItems",
                table: "CartItems",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "WishListItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WishListId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishListItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WishListItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishListItems_Products_ProductId1",
                        column: x => x.ProductId1,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WishListItems_WishLists_WishListId",
                        column: x => x.WishListId,
                        principalTable: "WishLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WishListItems_ProductId",
                table: "WishListItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WishListItems_ProductId1",
                table: "WishListItems",
                column: "ProductId1");

            migrationBuilder.CreateIndex(
                name: "IX_WishListItems_WishListId",
                table: "WishListItems",
                column: "WishListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WishListItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartItems",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "WishLists");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PaymentIntentId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PaymentIntentId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CartItems");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "WishLists",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Payments",
                newName: "PaymentDate");

            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "Payments",
                newName: "PaymentAmount");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Payments",
                newName: "PaymentStatus");

            migrationBuilder.AddColumn<int>(
                name: "ProductID",
                table: "WishLists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "PaymentId",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartItems",
                table: "CartItems",
                columns: new[] { "ProductId", "CartID" });

            migrationBuilder.CreateIndex(
                name: "IX_WishLists_ProductID",
                table: "WishLists",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_WishLists_Products_ProductID",
                table: "WishLists",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
