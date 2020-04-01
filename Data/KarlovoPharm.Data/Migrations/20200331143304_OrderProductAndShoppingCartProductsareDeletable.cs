using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KarlovoPharm.Data.Migrations
{
    public partial class OrderProductAndShoppingCartProductsareDeletable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "ShoppingCartProducts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "ShoppingCartProducts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "ShoppingCartProducts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ShoppingCartProducts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "ShoppingCartProducts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "OrdersProducts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "OrdersProducts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "OrdersProducts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "OrdersProducts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "OrdersProducts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DispatchDate",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EstimatedDeliveryDate",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderStatus",
                table: "Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartProducts_IsDeleted",
                table: "ShoppingCartProducts",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersProducts_IsDeleted",
                table: "OrdersProducts",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ShoppingCartProducts_IsDeleted",
                table: "ShoppingCartProducts");

            migrationBuilder.DropIndex(
                name: "IX_OrdersProducts_IsDeleted",
                table: "OrdersProducts");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "ShoppingCartProducts");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "ShoppingCartProducts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ShoppingCartProducts");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ShoppingCartProducts");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "ShoppingCartProducts");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "OrdersProducts");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "OrdersProducts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrdersProducts");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "OrdersProducts");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "OrdersProducts");

            migrationBuilder.DropColumn(
                name: "DispatchDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "EstimatedDeliveryDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderStatus",
                table: "Orders");
        }
    }
}
