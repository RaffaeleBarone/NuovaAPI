using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NuovaAPI.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prodotto_Ordini_IdOrdine",
                table: "Prodotto");

            migrationBuilder.DropForeignKey(
                name: "FK_Prodotto_Vetrina_IdVetrina",
                table: "Prodotto");

            migrationBuilder.AlterColumn<int>(
                name: "IdVetrina",
                table: "Prodotto",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "IdOrdine",
                table: "Prodotto",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Prodotto_Ordini_IdOrdine",
                table: "Prodotto",
                column: "IdOrdine",
                principalTable: "Ordini",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prodotto_Vetrina_IdVetrina",
                table: "Prodotto",
                column: "IdVetrina",
                principalTable: "Vetrina",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prodotto_Ordini_IdOrdine",
                table: "Prodotto");

            migrationBuilder.DropForeignKey(
                name: "FK_Prodotto_Vetrina_IdVetrina",
                table: "Prodotto");

            migrationBuilder.AlterColumn<int>(
                name: "IdVetrina",
                table: "Prodotto",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdOrdine",
                table: "Prodotto",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Prodotto_Ordini_IdOrdine",
                table: "Prodotto",
                column: "IdOrdine",
                principalTable: "Ordini",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prodotto_Vetrina_IdVetrina",
                table: "Prodotto",
                column: "IdVetrina",
                principalTable: "Vetrina",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
