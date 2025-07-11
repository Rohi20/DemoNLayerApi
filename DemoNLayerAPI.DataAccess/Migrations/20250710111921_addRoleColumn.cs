using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoNLayerApi.Data.Migrations
{
    public partial class addRoleColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "UserProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Internal");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "UserProfiles");
        }
    }
}
