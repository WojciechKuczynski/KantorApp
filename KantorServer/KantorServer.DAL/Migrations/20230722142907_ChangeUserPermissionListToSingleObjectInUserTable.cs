using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KantorServer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserPermissionListToSingleObjectInUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPermissions_Users_UserId",
                table: "UserPermissions");

            migrationBuilder.DropIndex(
                name: "IX_UserPermissions_UserId",
                table: "UserPermissions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserPermissions");

            migrationBuilder.AddColumn<long>(
                name: "PermissionId",
                table: "Users",
                type: "bigint",
                nullable: true,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PermissionId",
                table: "Users",
                column: "PermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserPermissions_PermissionId",
                table: "Users",
                column: "PermissionId",
                principalTable: "UserPermissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserPermissions_PermissionId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PermissionId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PermissionId",
                table: "Users");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "UserPermissions",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissions_UserId",
                table: "UserPermissions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPermissions_Users_UserId",
                table: "UserPermissions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
