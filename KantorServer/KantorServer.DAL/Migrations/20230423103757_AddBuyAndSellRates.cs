﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KantorServer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddBuyAndSellRates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MinimalRate",
                table: "Rates",
                newName: "MinimalSellRate");

            migrationBuilder.RenameColumn(
                name: "DefaultRate",
                table: "Rates",
                newName: "MinimalBuyRate");

            migrationBuilder.AddColumn<decimal>(
                name: "DefaultBuyRate",
                table: "Rates",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DefaultSellRate",
                table: "Rates",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultBuyRate",
                table: "Rates");

            migrationBuilder.DropColumn(
                name: "DefaultSellRate",
                table: "Rates");

            migrationBuilder.RenameColumn(
                name: "MinimalSellRate",
                table: "Rates",
                newName: "MinimalRate");

            migrationBuilder.RenameColumn(
                name: "MinimalBuyRate",
                table: "Rates",
                newName: "DefaultRate");
        }
    }
}
