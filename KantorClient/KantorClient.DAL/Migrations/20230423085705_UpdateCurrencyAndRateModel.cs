using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KantorClient.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCurrencyAndRateModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExternalId",
                table: "Rates",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ExternalId",
                table: "Currencies",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "Rates");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "Currencies");
        }
    }
}
