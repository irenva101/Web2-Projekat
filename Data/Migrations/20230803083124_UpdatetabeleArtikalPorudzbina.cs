using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class UpdatetabeleArtikalPorudzbina : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArtikliPorudzbina",
                columns: table => new
                {
                    ArtikalPorudzbinaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArtikliId = table.Column<int>(type: "int", nullable: false),
                    PorudzbineId = table.Column<int>(type: "int", nullable: false),
                    ArtikalId = table.Column<int>(type: "int", nullable: true),
                    PorudzbinaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtikliPorudzbina", x => x.ArtikalPorudzbinaId);
                    table.ForeignKey(
                        name: "FK_ArtikliPorudzbina_Artikli_ArtikalId",
                        column: x => x.ArtikalId,
                        principalTable: "Artikli",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ArtikliPorudzbina_Porudzbine_PorudzbinaId",
                        column: x => x.PorudzbinaId,
                        principalTable: "Porudzbine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtikliPorudzbina_ArtikalId",
                table: "ArtikliPorudzbina",
                column: "ArtikalId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtikliPorudzbina_PorudzbinaId",
                table: "ArtikliPorudzbina",
                column: "PorudzbinaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtikliPorudzbina");
        }
    }
}
