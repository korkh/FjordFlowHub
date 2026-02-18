using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreightService.Data.Migrations
{
    /// <inheritdoc />
    public partial class CargoDimensionsUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "WidthMeters",
                table: "cargos",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WidthMeters",
                table: "cargos");
        }
    }
}
