using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeathBringer.EntityFramework.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tabella_Categorie",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataCreazioneRecord = table.Column<DateTime>(nullable: false),
                    DataUltimaModifica = table.Column<DateTime>(nullable: false),
                    UtenteCreazioneRecord = table.Column<string>(nullable: true),
                    UtenteUltimaModificaRecord = table.Column<string>(nullable: true),
                    Nome = table.Column<string>(maxLength: 255, nullable: false),
                    Descrizione = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tabella_Categorie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tabella_Utenti",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataCreazioneRecord = table.Column<DateTime>(nullable: false),
                    DataUltimaModifica = table.Column<DateTime>(nullable: false),
                    UtenteCreazioneRecord = table.Column<string>(nullable: true),
                    UtenteUltimaModificaRecord = table.Column<string>(nullable: true),
                    Username = table.Column<string>(maxLength: 255, nullable: false),
                    Nome = table.Column<string>(maxLength: 255, nullable: false),
                    Cognome = table.Column<string>(maxLength: 255, nullable: false),
                    Email = table.Column<string>(maxLength: 255, nullable: false),
                    Indirizzo = table.Column<string>(nullable: true),
                    Civico = table.Column<string>(nullable: true),
                    Cap = table.Column<int>(nullable: false),
                    Citta = table.Column<string>(nullable: true),
                    Password = table.Column<string>(maxLength: 255, nullable: false),
                    IsAdministrator = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tabella_Utenti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tabella_Prodotti",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataCreazioneRecord = table.Column<DateTime>(nullable: false),
                    DataUltimaModifica = table.Column<DateTime>(nullable: false),
                    UtenteCreazioneRecord = table.Column<string>(nullable: true),
                    UtenteUltimaModificaRecord = table.Column<string>(nullable: true),
                    Nome = table.Column<string>(maxLength: 255, nullable: false),
                    CategoriaAppartenenzaId = table.Column<int>(nullable: false),
                    DataProduzione = table.Column<DateTime>(nullable: false),
                    Descrizione = table.Column<string>(nullable: true),
                    Foto = table.Column<byte[]>(nullable: true),
                    Brand = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tabella_Prodotti", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tabella_Prodotti_tabella_Categorie_CategoriaAppartenenzaId",
                        column: x => x.CategoriaAppartenenzaId,
                        principalTable: "tabella_Categorie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tabella_Prodotti_CategoriaAppartenenzaId",
                table: "tabella_Prodotti",
                column: "CategoriaAppartenenzaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tabella_Prodotti");

            migrationBuilder.DropTable(
                name: "tabella_Utenti");

            migrationBuilder.DropTable(
                name: "tabella_Categorie");
        }
    }
}
