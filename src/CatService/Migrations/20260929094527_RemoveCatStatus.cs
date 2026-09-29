using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatService.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCatStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "cats");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "cats",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "cats",
                keyColumn: "id",
                keyValue: 1,
                column: "status",
                value: 2);

            migrationBuilder.UpdateData(
                table: "cats",
                keyColumn: "id",
                keyValue: 2,
                column: "status",
                value: 2);

            migrationBuilder.UpdateData(
                table: "cats",
                keyColumn: "id",
                keyValue: 3,
                column: "status",
                value: 1);
        }
    }
}
