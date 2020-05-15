using Microsoft.EntityFrameworkCore.Migrations;

namespace VegeStore.Data.Migrations
{
    public partial class AddAmountToCoupons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DicountAmount",
                table: "Coupons",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DicountAmount",
                table: "Coupons");
        }
    }
}
