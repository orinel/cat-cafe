using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatService.Migrations
{
    /// <inheritdoc />
    public partial class RenameActivityStartedAtToActivityStartsAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "activity_started_at",
                table: "cats",
                newName: "activity_starts_at");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "activity_starts_at",
                table: "cats",
                newName: "activity_started_at");
        }
    }
}
