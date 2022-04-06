using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserApi.DAL.Migrations
{
    public partial class ChangeKeyUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName_Name",
                table: "Users",
                columns: new[] { "UserName", "Name" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_UserName_Name",
                table: "Users");
        }
    }
}
