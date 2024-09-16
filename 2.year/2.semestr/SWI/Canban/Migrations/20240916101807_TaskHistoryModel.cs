using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Canban.Migrations
{
    /// <inheritdoc />
    public partial class TaskHistoryModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OldTaskInfoId = table.Column<int>(type: "INTEGER", nullable: false),
                    NewTaskInfoId = table.Column<int>(type: "INTEGER", nullable: false),
                    OldColumnId = table.Column<int>(type: "INTEGER", nullable: false),
                    NewColumnId = table.Column<int>(type: "INTEGER", nullable: false),
                    MoveAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MoveById = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskHistories_Tasks_NewTaskInfoId",
                        column: x => x.NewTaskInfoId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskHistories_Tasks_OldTaskInfoId",
                        column: x => x.OldTaskInfoId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskHistories_Users_MoveById",
                        column: x => x.MoveById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskHistories_MoveById",
                table: "TaskHistories",
                column: "MoveById");

            migrationBuilder.CreateIndex(
                name: "IX_TaskHistories_NewTaskInfoId",
                table: "TaskHistories",
                column: "NewTaskInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskHistories_OldTaskInfoId",
                table: "TaskHistories",
                column: "OldTaskInfoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskHistories");
        }
    }
}
