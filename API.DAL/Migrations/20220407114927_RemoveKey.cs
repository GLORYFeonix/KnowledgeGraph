using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserApi.DAL.Migrations
{
    public partial class RemoveKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AutoLoginTag",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AutoLoginTag",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Token",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
