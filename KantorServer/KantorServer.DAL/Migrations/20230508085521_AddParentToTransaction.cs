using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KantorServer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddParentToTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Parent",
                table: "Transactions",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Parent",
                table: "Transactions");
        }
    }
}
