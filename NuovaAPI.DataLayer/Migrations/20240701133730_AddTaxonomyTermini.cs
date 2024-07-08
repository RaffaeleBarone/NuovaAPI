using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NuovaAPI.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddTaxonomyTermini : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodiceProdotto",
                table: "Prodotto",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Taxonomy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxonomy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Termini",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Labels_en_US = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Labels_fr_FR = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Labels_it_IT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxonomyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Termini", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Termini_Taxonomy_TaxonomyId",
                        column: x => x.TaxonomyId,
                        principalTable: "Taxonomy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Termini_TaxonomyId",
                table: "Termini",
                column: "TaxonomyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Termini");

            migrationBuilder.DropTable(
                name: "Taxonomy");

            migrationBuilder.DropColumn(
                name: "CodiceProdotto",
                table: "Prodotto");
        }
    }
}
