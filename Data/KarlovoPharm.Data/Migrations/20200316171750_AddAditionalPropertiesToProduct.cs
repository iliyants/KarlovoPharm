namespace KarlovoPharm.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddAditionalPropertiesToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "UsersAddresses",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "UsersAddresses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "UsersAddresses",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UsersAddresses",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "UsersAddresses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Composition",
                table: "Products",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CountryOfOrigin",
                table: "Products",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Designation",
                table: "Products",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Effect",
                table: "Products",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Manufacturer",
                table: "Products",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WayOfuse",
                table: "Products",
                maxLength: 500,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersAddresses_IsDeleted",
                table: "UsersAddresses",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UsersAddresses_IsDeleted",
                table: "UsersAddresses");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "UsersAddresses");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "UsersAddresses");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UsersAddresses");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UsersAddresses");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "UsersAddresses");

            migrationBuilder.DropColumn(
                name: "Composition",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CountryOfOrigin",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Designation",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Effect",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Manufacturer",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "WayOfuse",
                table: "Products");
        }
    }
}
