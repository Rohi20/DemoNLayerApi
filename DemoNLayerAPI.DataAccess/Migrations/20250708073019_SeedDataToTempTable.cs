using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoNLayerApi.Data.Migrations
{
    public partial class SeedDataToTempTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BookCategory",
                columns: new[] { "BooksId", "CategoriesId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "BookCategory",
                columns: new[] { "BooksId", "CategoriesId" },
                values: new object[] { 1, 2 });

            migrationBuilder.InsertData(
                table: "BookCategory",
                columns: new[] { "BooksId", "CategoriesId" },
                values: new object[] { 2, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BookCategory",
                keyColumns: new[] { "BooksId", "CategoriesId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "BookCategory",
                keyColumns: new[] { "BooksId", "CategoriesId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "BookCategory",
                keyColumns: new[] { "BooksId", "CategoriesId" },
                keyValues: new object[] { 2, 1 });
        }
    }
}
