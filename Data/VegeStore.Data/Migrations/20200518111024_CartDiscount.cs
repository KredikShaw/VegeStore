using Microsoft.EntityFrameworkCore.Migrations;

namespace VegeStore.Data.Migrations
{
    public partial class CartDiscount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CouponApplied",
                table: "Carts");

            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "Carts",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Carts");

            migrationBuilder.AddColumn<bool>(
                name: "CouponApplied",
                table: "Carts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
