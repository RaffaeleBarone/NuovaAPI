using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NuovaAPI.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class Sixth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CodiceOrdine",
                table: "Ordini",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodiceOrdine",
                table: "Ordini");
        }
    }
}
