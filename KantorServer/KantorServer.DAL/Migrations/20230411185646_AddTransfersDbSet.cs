using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KantorServer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddTransfersDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Valid",
                table: "Rates",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Transfers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransferType = table.Column<int>(type: "int", nullable: false),
                    KantorId = table.Column<long>(type: "bigint", nullable: false),
                    TransferValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransferCurrencyId = table.Column<long>(type: "bigint", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transfers_Currencies_TransferCurrencyId",
                        column: x => x.TransferCurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transfers_Kantors_KantorId",
                        column: x => x.KantorId,
                        principalTable: "Kantors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_KantorId",
                table: "Transfers",
                column: "KantorId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_TransferCurrencyId",
                table: "Transfers",
                column: "TransferCurrencyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transfers");

            migrationBuilder.DropColumn(
                name: "Valid",
                table: "Rates");
        }
    }
}
