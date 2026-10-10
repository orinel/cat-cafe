using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CatService.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSeedCats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "activity_starts_at",
                table: "cats",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "activity_ends_at",
                table: "cats",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "activity_starts_at",
                table: "cats",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "activity_ends_at",
                table: "cats",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.InsertData(
                table: "cats",
                columns: new[] { "id", "activity", "activity_ends_at", "activity_starts_at", "age", "breed", "external_activity_id", "gender", "name" },
                values: new object[,]
                {
                    { 1, 2, null, null, 3, 0, null, 0, "Барсик" },
                    { 2, 4, new DateTimeOffset(new DateTime(2026, 10, 2, 13, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 10, 2, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 5, 0, 123123, 1, "Мурка" },
                    { 3, 3, null, null, 2, 0, null, 0, "Васька" }
                });
        }
    }
}
