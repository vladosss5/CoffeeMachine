using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeMachine.Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenamePurechaseToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Purchases",
                newName: "Orders");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_MachineId",
                table: "Orders",
                newName: "IX_Orders_MachineId");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_CoffeeId",
                table: "Orders",
                newName: "IX_Orders_CoffeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Purchases");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_MachineId",
                table: "Purchases",
                newName: "IX_Purchases_MachineId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_CoffeeId",
                table: "Purchases",
                newName: "IX_Purchases_CoffeeId");
        }
    }
}
