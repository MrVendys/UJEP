using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Canban.Migrations
{
    /// <inheritdoc />
    public partial class TaskModelUserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Statuses_StatusModelId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Tasks_TaskModelId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_TaskModelId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_StatusModelId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "TaskModelId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "StatusModelId",
                table: "Tasks");

            migrationBuilder.CreateTable(
                name: "TaskModelUserModel",
                columns: table => new
                {
                    TaskModelsId = table.Column<int>(type: "INTEGER", nullable: false),
                    UsersId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskModelUserModel", x => new { x.TaskModelsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_TaskModelUserModel_Tasks_TaskModelsId",
                        column: x => x.TaskModelsId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskModelUserModel_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskModelUserModel_UsersId",
                table: "TaskModelUserModel",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskModelUserModel");

            migrationBuilder.AddColumn<int>(
                name: "TaskModelId",
                table: "Users",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Tasks",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusModelId",
                table: "Tasks",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_TaskModelId",
                table: "Users",
                column: "TaskModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_StatusModelId",
                table: "Tasks",
                column: "StatusModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Statuses_StatusModelId",
                table: "Tasks",
                column: "StatusModelId",
                principalTable: "Statuses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Tasks_TaskModelId",
                table: "Users",
                column: "TaskModelId",
                principalTable: "Tasks",
                principalColumn: "Id");
        }
    }
}
