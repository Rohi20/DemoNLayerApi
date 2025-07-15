using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoNLayerApi.Data.Migrations
{
    public partial class SeedDataToBookTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "Description", "Price", "Title" },
                values: new object[,]
                {
                    { 10, 5, "A sweeping historical romance set during WWII, where love and sacrifice intertwine.", 14.99m, "Beneath the Crimson Sky" },
                    { 11, 6, "A fast-paced techno-thriller about a hacker who uncovers a government conspiracy.", 11.50m, "Digital Shadows" },
                    { 12, 7, "A lyrical tale of loss, memory, and finding one's roots in an ancestral home.", 13.25m, "The Garden of Echoes" },
                    { 13, 8, "In a noir-inspired metropolis, a detective must unravel a string of mysterious disappearances.", 12.00m, "City of Broken Glass" },
                    { 14, 9, "A poignant coming-of-age story set in southern India during the monsoon season.", 10.75m, "Monsoon Whispers" },
                    { 15, 10, "An adventure novel centered on a treasure-hunting expedition gone awry.", 15.00m, "The Tides of Isla Roja" },
                    { 16, 11, "A historical fantasy blending martial arts, court intrigue, and ancient magic.", 14.50m, "Silk and Steel" },
                    { 17, 12, "A speculative fiction novel where destiny is determined by an AI system.", 13.99m, "The Algorithm of Fate" },
                    { 18, 13, "A poetic novel exploring family honor, tradition, and rebellion in the Middle East.", 12.95m, "Veil of Jasmine" },
                    { 19, 14, "A Nordic mystery set in a remote village where the past resurfaces with deadly intent.", 11.80m, "Frozen Fjords" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 19);
        }
    }
}
