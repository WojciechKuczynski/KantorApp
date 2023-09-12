using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KantorClient.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddNotesToTransfer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Transfers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Transfers");
        }
    }
}
