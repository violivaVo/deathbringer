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
                name: "tabella_Utenti",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataCreazioneRecord = table.Column<DateTime>(nullable: false),
                    DataUltimaModifica = table.Column<DateTime>(nullable: false),
                    UtenteCreazioneRecord = table.Column<string>(nullable: true),
                    UtenteUltimaModificaRecord = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Nome = table.Column<string>(nullable: true),
                    Cognome = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Indirizzo = table.Column<string>(nullable: true),
                    Civico = table.Column<string>(nullable: true),
                    Cap = table.Column<int>(nullable: false),
                    Citta = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    IsAdministrator = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tabella_Utenti", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tabella_Utenti");
        }
    }
}
