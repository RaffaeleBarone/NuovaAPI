using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NuovaAPI.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddedOrdineProdotto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prodotto_Ordini_IdOrdine",
                table: "Prodotto");

            migrationBuilder.DropIndex(
                name: "IX_Prodotto_IdOrdine",
                table: "Prodotto");

            migrationBuilder.DropColumn(
                name: "IdOrdine",
                table: "Prodotto");

            migrationBuilder.RenameColumn(
                name: "Quantita",
                table: "Prodotto",
                newName: "QuantitaDisponibile");

            migrationBuilder.CreateTable(
                name: "OrdiniProdotti",
                columns: table => new
                {
                    IdOrdine = table.Column<int>(type: "int", nullable: false),
                    IdProdotto = table.Column<int>(type: "int", nullable: false),
                    QuantitaAcquistata = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdiniProdotti", x => new { x.IdOrdine, x.IdProdotto });
                    table.ForeignKey(
                        name: "FK_OrdiniProdotti_Ordini_IdOrdine",
                        column: x => x.IdOrdine,
                        principalTable: "Ordini",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdiniProdotti_Prodotto_IdProdotto",
                        column: x => x.IdProdotto,
                        principalTable: "Prodotto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrdiniProdotti_IdProdotto",
                table: "OrdiniProdotti",
                column: "IdProdotto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdiniProdotti");

            migrationBuilder.RenameColumn(
                name: "QuantitaDisponibile",
                table: "Prodotto",
                newName: "Quantita");

            migrationBuilder.AddColumn<int>(
                name: "IdOrdine",
                table: "Prodotto",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prodotto_IdOrdine",
                table: "Prodotto",
                column: "IdOrdine");

            migrationBuilder.AddForeignKey(
                name: "FK_Prodotto_Ordini_IdOrdine",
                table: "Prodotto",
                column: "IdOrdine",
                principalTable: "Ordini",
                principalColumn: "Id");
        }
    }
}
