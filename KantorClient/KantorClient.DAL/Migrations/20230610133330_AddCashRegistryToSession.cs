using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KantorClient.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddCashRegistryToSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Cash",
                table: "UserSessions",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cash",
                table: "UserSessions");
        }
    }
}
