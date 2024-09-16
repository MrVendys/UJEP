using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Canban.Migrations
{
    /// <inheritdoc />
    public partial class updateTaskHistorieTaskId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TaskId",
                table: "TaskHistories",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaskId",
                table: "TaskHistories");
        }
    }
}
