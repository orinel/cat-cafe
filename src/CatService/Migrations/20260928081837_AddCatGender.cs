using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatService.Migrations
{
    /// <inheritdoc />
    public partial class AddCatGender : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "gender",
                table: "cats",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "cats",
                keyColumn: "id",
                keyValue: 1,
                column: "gender",
                value: 0);

            migrationBuilder.UpdateData(
                table: "cats",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "gender", "name" },
                values: new object[] { 1, "Мурка" });

            migrationBuilder.UpdateData(
                table: "cats",
                keyColumn: "id",
                keyValue: 3,
                column: "gender",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "gender",
                table: "cats");

            migrationBuilder.UpdateData(
                table: "cats",
                keyColumn: "id",
                keyValue: 2,
                column: "name",
                value: "Мурзик");
        }
    }
}
