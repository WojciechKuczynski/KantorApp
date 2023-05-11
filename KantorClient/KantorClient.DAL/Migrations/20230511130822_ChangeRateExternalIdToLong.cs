using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KantorClient.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRateExternalIdToLong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "ExternalId",
                table: "Rates",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ExternalId",
                table: "Rates",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(long),
                oldType: "INTEGER",
                oldNullable: true);
        }
    }
}
