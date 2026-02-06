using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreightService.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameHighBidToLowBid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReserveNotMet",
                table: "OutboxState",
                newName: "Delivered");

            migrationBuilder.RenameColumn(
                name: "ReserveNotMet",
                table: "InboxState",
                newName: "Delivered");

            migrationBuilder.RenameIndex(
                name: "IX_InboxState_ReserveNotMet",
                table: "InboxState",
                newName: "IX_InboxState_Delivered");

            migrationBuilder.AlterColumn<int>(
                name: "SoldAmount",
                table: "freights",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "CurrentLowBid",
                table: "freights",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Delivered",
                table: "OutboxState",
                newName: "ReserveNotMet");

            migrationBuilder.RenameColumn(
                name: "Delivered",
                table: "InboxState",
                newName: "ReserveNotMet");

            migrationBuilder.RenameIndex(
                name: "IX_InboxState_Delivered",
                table: "InboxState",
                newName: "IX_InboxState_ReserveNotMet");

            migrationBuilder.AlterColumn<int>(
                name: "SoldAmount",
                table: "freights",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CurrentLowBid",
                table: "freights",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
