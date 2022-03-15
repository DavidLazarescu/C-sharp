using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_learning.Data.Migrations
{
    public partial class AppUserToUserRenamingMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "Users",
                newName: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Users",
                newName: "AppUserId");
        }
    }
}
