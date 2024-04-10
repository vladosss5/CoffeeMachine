using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CoffeeMachine.Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Banknotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Par = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("money_pk", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Coffees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("coffee_pk", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Machines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    SerialNumber = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("machine_pk", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BanknotesMachines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    IdMachine = table.Column<int>(type: "integer", nullable: false),
                    IdBanknote = table.Column<int>(type: "integer", nullable: false),
                    CountBanknotes = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("banknotes_machines_pk", x => x.Id);
                    table.ForeignKey(
                        name: "banknotes_machines_banknote_fk",
                        column: x => x.IdBanknote,
                        principalTable: "Banknotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "banknotes_machines_machine_fk",
                        column: x => x.IdMachine,
                        principalTable: "Machines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IdCoffee = table.Column<int>(type: "integer", nullable: false),
                    IdMachine = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("purchase_pk", x => x.Id);
                    table.ForeignKey(
                        name: "purchase_coffee_fk",
                        column: x => x.IdCoffee,
                        principalTable: "Coffees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "purchase_machine_fk",
                        column: x => x.IdMachine,
                        principalTable: "Machines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Type = table.Column<bool>(type: "boolean", nullable: false),
                    CountBanknotes = table.Column<int>(type: "integer", nullable: false),
                    IdBanknote = table.Column<int>(type: "integer", nullable: false),
                    IdPurchase = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("transaction_pk", x => x.Id);
                    table.ForeignKey(
                        name: "transaction_banknote_fk",
                        column: x => x.IdBanknote,
                        principalTable: "Banknotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "transaction_purchase_fk",
                        column: x => x.IdPurchase,
                        principalTable: "Purchases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BanknotesMachines_IdBanknote",
                table: "BanknotesMachines",
                column: "IdBanknote");

            migrationBuilder.CreateIndex(
                name: "IX_BanknotesMachines_IdMachine",
                table: "BanknotesMachines",
                column: "IdMachine");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_IdCoffee",
                table: "Purchases",
                column: "IdCoffee");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_IdMachine",
                table: "Purchases",
                column: "IdMachine");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_IdBanknote",
                table: "Transactions",
                column: "IdBanknote");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_IdPurchase",
                table: "Transactions",
                column: "IdPurchase");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BanknotesMachines");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Banknotes");

            migrationBuilder.DropTable(
                name: "Purchases");

            migrationBuilder.DropTable(
                name: "Coffees");

            migrationBuilder.DropTable(
                name: "Machines");
        }
    }
}
