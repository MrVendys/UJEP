using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Canban.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHistoryModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OldUsersNames",
                table: "TaskHistories",
                newName: "OldUsers");

            migrationBuilder.RenameColumn(
                name: "NewUsersNames",
                table: "TaskHistories",
                newName: "NewUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OldUsers",
                table: "TaskHistories",
                newName: "OldUsersNames");

            migrationBuilder.RenameColumn(
                name: "NewUsers",
                table: "TaskHistories",
                newName: "NewUsersNames");
        }
    }
}
