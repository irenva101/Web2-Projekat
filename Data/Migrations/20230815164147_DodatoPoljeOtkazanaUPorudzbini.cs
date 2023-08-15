using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class DodatoPoljeOtkazanaUPorudzbini : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Otkazana",
                table: "Porudzbine",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Otkazana",
                table: "Porudzbine");
        }
    }
}
