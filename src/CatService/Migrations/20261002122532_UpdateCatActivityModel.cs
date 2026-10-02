using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCatActivityModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "cats",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "activity_ends_at", "activity_started_at", "external_activity_id" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 10, 2, 13, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 10, 2, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 123123 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "cats",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "activity_ends_at", "activity_started_at", "external_activity_id" },
                values: new object[] { null, null, null });
        }
    }
}
