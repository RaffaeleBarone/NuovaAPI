using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NuovaAPI.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class ModificaTermini : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Labels_en_US",
                table: "Termini");

            migrationBuilder.RenameColumn(
                name: "Labels_it_IT",
                table: "Termini",
                newName: "Traduzione");

            migrationBuilder.RenameColumn(
                name: "Labels_fr_FR",
                table: "Termini",
                newName: "Lingua");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Traduzione",
                table: "Termini",
                newName: "Labels_it_IT");

            migrationBuilder.RenameColumn(
                name: "Lingua",
                table: "Termini",
                newName: "Labels_fr_FR");

            migrationBuilder.AddColumn<string>(
                name: "Labels_en_US",
                table: "Termini",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
