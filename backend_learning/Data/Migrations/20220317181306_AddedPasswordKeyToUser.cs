using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_learning.Data.Migrations
{
    public partial class AddedPasswordKeyToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSeed",
                table: "Users",
                type: "BLOB",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordSeed",
                table: "Users");
        }
    }
}
