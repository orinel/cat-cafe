using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CatService.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialCats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "cats",
                columns: new[] { "id", "age", "breed", "name", "status" },
                values: new object[,]
                {
                    { 1, 3, 0, "Барсик", 2 },
                    { 2, 5, 0, "Мурзик", 2 },
                    { 3, 2, 0, "Васька", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "cats",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "cats",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "cats",
                keyColumn: "id",
                keyValue: 3);
        }
    }
}
