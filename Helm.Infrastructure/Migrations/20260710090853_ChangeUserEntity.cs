using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Helm.Core.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_ADLogin",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ADLogin",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Hash",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Users");

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "ADLogin",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Hash",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: -2147483648);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ADLogin",
                table: "Users",
                column: "ADLogin",
                unique: true);
        }
    }
}
