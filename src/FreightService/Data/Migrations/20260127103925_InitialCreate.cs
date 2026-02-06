using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreightService.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "freights",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReservePrice = table.Column<int>(type: "integer", nullable: false),
                    Seller = table.Column<string>(type: "text", nullable: true),
                    Winner = table.Column<string>(type: "text", nullable: true),
                    SoldAmount = table.Column<int>(type: "integer", nullable: false),
                    CurrentLowBid = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                    UpdatedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                    AuctionEnd = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                    Status = table.Column<int>(type: "integer", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_freights", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "cargos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    WeightKg = table.Column<int>(type: "integer", nullable: false),
                    LengthMeters = table.Column<double>(type: "double precision", nullable: false),
                    HeightMeters = table.Column<double>(type: "double precision", nullable: false),
                    PickupCity = table.Column<string>(type: "text", nullable: true),
                    DeliveryCity = table.Column<string>(type: "text", nullable: true),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    FreightId = table.Column<Guid>(type: "uuid", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cargos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cargos_freights_FreightId",
                        column: x => x.FreightId,
                        principalTable: "freights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_cargos_FreightId",
                table: "cargos",
                column: "FreightId",
                unique: true
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "cargos");

            migrationBuilder.DropTable(name: "freights");
        }
    }
}
