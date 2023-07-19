using Microsoft.EntityFrameworkCore.Migrations;

namespace WEB2_Projekat.Migrations
{
    public partial class IspravakTabeleKorisnik : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "tipKorisnika",
                table: "Korisnici",
                newName: "TipKorisnika");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TipKorisnika",
                table: "Korisnici",
                newName: "tipKorisnika");
        }
    }
}
