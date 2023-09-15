using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class DodajemOrderIdUPorudzbine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Porudzbine",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Porudzbine");
        }
    }
}
