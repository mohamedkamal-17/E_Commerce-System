using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_commerceManagementSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class updateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentCategoryID",
                table: "Categories");

            migrationBuilder.AlterColumn<int>(
                name: "TrackingNumber",
                table: "Shipping",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Shipping",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "ReorderLevel",
                table: "Inventory",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImgUrel",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Shipping_UserId",
                table: "Shipping",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shipping_AspNetUsers_UserId",
                table: "Shipping",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shipping_AspNetUsers_UserId",
                table: "Shipping");

            migrationBuilder.DropIndex(
                name: "IX_Shipping_UserId",
                table: "Shipping");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Shipping");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ImgUrel",
                table: "Categories");

            migrationBuilder.AlterColumn<string>(
                name: "TrackingNumber",
                table: "Shipping",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ReorderLevel",
                table: "Inventory",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ParentCategoryID",
                table: "Categories",
                type: "int",
                nullable: true);
        }
    }
}
