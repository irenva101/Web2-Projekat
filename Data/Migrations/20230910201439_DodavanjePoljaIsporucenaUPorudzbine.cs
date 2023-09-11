using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class DodavanjePoljaIsporucenaUPorudzbine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Isporucena",
                table: "Porudzbine",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Isporucena",
                table: "Porudzbine");
        }
    }
}
