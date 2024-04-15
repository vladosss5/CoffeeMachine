using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeMachine.Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class DeleteCountBanknoteToTransactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountBanknotes",
                table: "Transactions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountBanknotes",
                table: "Transactions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
