namespace KarlovoPharm.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class UserFavouriteProductIsDeletable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "UserFavouriteProducts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "UserFavouriteProducts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "UserFavouriteProducts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UserFavouriteProducts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "UserFavouriteProducts",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PostCode",
                table: "Addresses",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Addresses",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BuildingNumber",
                table: "Addresses",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.CreateIndex(
                name: "IX_UserFavouriteProducts_IsDeleted",
                table: "UserFavouriteProducts",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserFavouriteProducts_IsDeleted",
                table: "UserFavouriteProducts");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "UserFavouriteProducts");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "UserFavouriteProducts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserFavouriteProducts");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UserFavouriteProducts");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "UserFavouriteProducts");

            migrationBuilder.AlterColumn<string>(
                name: "PostCode",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Addresses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BuildingNumber",
                table: "Addresses",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 10,
                oldNullable: true);
        }
    }
}
