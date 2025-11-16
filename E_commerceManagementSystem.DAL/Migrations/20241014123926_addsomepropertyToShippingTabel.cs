using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_commerceManagementSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addsomepropertyToShippingTabel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TrackingNumber",
                table: "Shipping",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TrackingNumber",
                table: "Shipping",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
