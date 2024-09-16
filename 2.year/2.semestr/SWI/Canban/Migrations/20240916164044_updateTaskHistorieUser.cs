using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Canban.Migrations
{
    /// <inheritdoc />
    public partial class updateTaskHistorieUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskHistories_Users_MoveById",
                table: "TaskHistories");

            migrationBuilder.DropIndex(
                name: "IX_TaskHistories_MoveById",
                table: "TaskHistories");

            migrationBuilder.DropColumn(
                name: "MoveById",
                table: "TaskHistories");

            migrationBuilder.AlterColumn<string>(
                name: "NewName",
                table: "TaskHistories",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "MoveBy",
                table: "TaskHistories",
                type: "TEXT",
                nullable: true,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MoveBy",
                table: "TaskHistories");

            migrationBuilder.AlterColumn<string>(
                name: "NewName",
                table: "TaskHistories",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MoveById",
                table: "TaskHistories",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskHistories_MoveById",
                table: "TaskHistories",
                column: "MoveById");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskHistories_Users_MoveById",
                table: "TaskHistories",
                column: "MoveById",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
