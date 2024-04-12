using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CoffeeMachine.Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class ReturnEntityBanknoteMachine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BanknoteMachine");

            migrationBuilder.CreateTable(
                name: "BanknotesMachines",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    MachineId = table.Column<long>(type: "bigint", nullable: false),
                    BanknoteId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("banknotes_machines_pk", x => x.Id);
                    table.ForeignKey(
                        name: "banknotes_machines_banknote_fk",
                        column: x => x.BanknoteId,
                        principalTable: "Banknotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "banknotes_machines_machine_fk",
                        column: x => x.MachineId,
                        principalTable: "Machines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BanknotesMachines_BanknoteId",
                table: "BanknotesMachines",
                column: "BanknoteId");

            migrationBuilder.CreateIndex(
                name: "IX_BanknotesMachines_MachineId",
                table: "BanknotesMachines",
                column: "MachineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BanknotesMachines");

            migrationBuilder.CreateTable(
                name: "BanknoteMachine",
                columns: table => new
                {
                    BanknotesId = table.Column<long>(type: "bigint", nullable: false),
                    MachinesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BanknoteMachine", x => new { x.BanknotesId, x.MachinesId });
                    table.ForeignKey(
                        name: "FK_BanknoteMachine_Banknotes_BanknotesId",
                        column: x => x.BanknotesId,
                        principalTable: "Banknotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BanknoteMachine_Machines_MachinesId",
                        column: x => x.MachinesId,
                        principalTable: "Machines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BanknoteMachine_MachinesId",
                table: "BanknoteMachine",
                column: "MachinesId");
        }
    }
}
