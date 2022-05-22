using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task_Portal.Migrations
{
    public partial class U_Pass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "LoginUserData",
                newName: "LoginPassword");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LoginPassword",
                table: "LoginUserData",
                newName: "Password");
        }
    }
}
