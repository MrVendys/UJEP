using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Canban.Migrations
{
    /// <inheritdoc />
    public partial class DeleteUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TaskModelId",
                table: "Users",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_TaskModelId",
                table: "Users",
                column: "TaskModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Tasks_TaskModelId",
                table: "Users",
                column: "TaskModelId",
                principalTable: "Tasks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Tasks_TaskModelId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_TaskModelId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TaskModelId",
                table: "Users");
        }
    }
}
