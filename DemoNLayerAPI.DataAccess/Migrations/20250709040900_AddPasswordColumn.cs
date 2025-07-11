using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoNLayerApi.Data.Migrations
{
    public partial class AddPasswordColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "UserProfiles",
                newName: "Password");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "UserProfiles",
                newName: "Name");
        }
    }
}
