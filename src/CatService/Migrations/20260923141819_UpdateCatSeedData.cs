using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCatSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "cats",
                keyColumn: "id",
                keyValue: 1,
                column: "activity",
                value: 2);

            migrationBuilder.UpdateData(
                table: "cats",
                keyColumn: "id",
                keyValue: 2,
                column: "activity",
                value: 4);

            migrationBuilder.UpdateData(
                table: "cats",
                keyColumn: "id",
                keyValue: 3,
                column: "activity",
                value: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "cats",
                keyColumn: "id",
                keyValue: 1,
                column: "activity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "cats",
                keyColumn: "id",
                keyValue: 2,
                column: "activity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "cats",
                keyColumn: "id",
                keyValue: 3,
                column: "activity",
                value: 0);
        }
    }
}
