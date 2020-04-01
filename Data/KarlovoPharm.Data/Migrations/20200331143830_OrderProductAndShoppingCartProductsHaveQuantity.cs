using Microsoft.EntityFrameworkCore.Migrations;

namespace KarlovoPharm.Data.Migrations
{
    public partial class OrderProductAndShoppingCartProductsHaveQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "ShoppingCartProducts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "OrdersProducts",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ShoppingCartProducts");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "OrdersProducts");
        }
    }
}
