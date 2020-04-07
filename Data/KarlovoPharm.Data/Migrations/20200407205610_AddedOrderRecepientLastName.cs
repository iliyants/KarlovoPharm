using Microsoft.EntityFrameworkCore.Migrations;

namespace KarlovoPharm.Data.Migrations
{
    public partial class AddedOrderRecepientLastName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RecipientLastName",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecipientLastName",
                table: "Orders");
        }
    }
}
