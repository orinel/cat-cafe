using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatService.Migrations
{
    /// <inheritdoc />
    public partial class AddCatActivity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "activity",
                table: "cats",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "activity",
                table: "cats");
        }
    }
}
