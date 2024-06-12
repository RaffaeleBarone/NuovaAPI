using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NuovaAPI.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cognome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataDiNascita = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vetrina",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodiceVetrina = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vetrina", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ordini",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordini", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ordini_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prodotto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeProdotto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezzo = table.Column<float>(type: "real", nullable: false),
                    Quantita = table.Column<int>(type: "int", nullable: false),
                    IdVetrina = table.Column<int>(type: "int", nullable: false),
                    IdOrdine = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prodotto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prodotto_Ordini_IdOrdine",
                        column: x => x.IdOrdine,
                        principalTable: "Ordini",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prodotto_Vetrina_IdVetrina",
                        column: x => x.IdVetrina,
                        principalTable: "Vetrina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ordini_ClienteId",
                table: "Ordini",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Prodotto_IdOrdine",
                table: "Prodotto",
                column: "IdOrdine");

            migrationBuilder.CreateIndex(
                name: "IX_Prodotto_IdVetrina",
                table: "Prodotto",
                column: "IdVetrina");

            migrationBuilder.CreateIndex(
                name: "IX_Vetrina_CodiceVetrina",
                table: "Vetrina",
                column: "CodiceVetrina",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prodotto");

            migrationBuilder.DropTable(
                name: "Ordini");

            migrationBuilder.DropTable(
                name: "Vetrina");

            migrationBuilder.DropTable(
                name: "Cliente");
        }
    }
}
