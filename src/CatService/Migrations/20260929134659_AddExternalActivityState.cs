using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatService.Migrations
{
    /// <inheritdoc />
    public partial class AddExternalActivityState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "activity_ends_at",
                table: "cats",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "activity_started_at",
                table: "cats",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "external_activity_id",
                table: "cats",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "cats",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "activity_ends_at", "activity_started_at", "external_activity_id" },
                values: new object[] { null, null, null });

            migrationBuilder.UpdateData(
                table: "cats",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "activity_ends_at", "activity_started_at", "external_activity_id" },
                values: new object[] { null, null, null });

            migrationBuilder.UpdateData(
                table: "cats",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "activity_ends_at", "activity_started_at", "external_activity_id" },
                values: new object[] { null, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "activity_ends_at",
                table: "cats");

            migrationBuilder.DropColumn(
                name: "activity_started_at",
                table: "cats");

            migrationBuilder.DropColumn(
                name: "external_activity_id",
                table: "cats");
        }
    }
}
