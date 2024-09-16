using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Canban.Migrations
{
    /// <inheritdoc />
    public partial class updateTaskHistory2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskHistories_Tasks_NewTaskInfoId",
                table: "TaskHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskHistories_Tasks_OldTaskInfoId",
                table: "TaskHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskHistories_Users_MoveById",
                table: "TaskHistories");

            migrationBuilder.DropIndex(
                name: "IX_TaskHistories_NewTaskInfoId",
                table: "TaskHistories");

            migrationBuilder.DropIndex(
                name: "IX_TaskHistories_OldTaskInfoId",
                table: "TaskHistories");

            migrationBuilder.RenameColumn(
                name: "OldTaskInfoId",
                table: "TaskHistories",
                newName: "OldColumnId");

            migrationBuilder.RenameColumn(
                name: "NewTaskInfoId",
                table: "TaskHistories",
                newName: "NewColumnId");

            migrationBuilder.AlterColumn<int>(
                name: "MoveById",
                table: "TaskHistories",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<DateTime>(
                name: "MoveAt",
                table: "TaskHistories",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AddColumn<DateTime>(
                name: "NewCompleted",
                table: "TaskHistories",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NewDeadline",
                table: "TaskHistories",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NewDesc",
                table: "TaskHistories",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NewName",
                table: "TaskHistories",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "NewStarted",
                table: "TaskHistories",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NewUsersNames",
                table: "TaskHistories",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OldCompleted",
                table: "TaskHistories",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OldDeadline",
                table: "TaskHistories",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OldDesc",
                table: "TaskHistories",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OldName",
                table: "TaskHistories",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "OldStarted",
                table: "TaskHistories",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OldUsersNames",
                table: "TaskHistories",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskHistories_Users_MoveById",
                table: "TaskHistories",
                column: "MoveById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskHistories_Users_MoveById",
                table: "TaskHistories");

            migrationBuilder.DropColumn(
                name: "NewCompleted",
                table: "TaskHistories");

            migrationBuilder.DropColumn(
                name: "NewDeadline",
                table: "TaskHistories");

            migrationBuilder.DropColumn(
                name: "NewDesc",
                table: "TaskHistories");

            migrationBuilder.DropColumn(
                name: "NewName",
                table: "TaskHistories");

            migrationBuilder.DropColumn(
                name: "NewStarted",
                table: "TaskHistories");

            migrationBuilder.DropColumn(
                name: "NewUsersNames",
                table: "TaskHistories");

            migrationBuilder.DropColumn(
                name: "OldCompleted",
                table: "TaskHistories");

            migrationBuilder.DropColumn(
                name: "OldDeadline",
                table: "TaskHistories");

            migrationBuilder.DropColumn(
                name: "OldDesc",
                table: "TaskHistories");

            migrationBuilder.DropColumn(
                name: "OldName",
                table: "TaskHistories");

            migrationBuilder.DropColumn(
                name: "OldStarted",
                table: "TaskHistories");

            migrationBuilder.DropColumn(
                name: "OldUsersNames",
                table: "TaskHistories");

            migrationBuilder.RenameColumn(
                name: "OldColumnId",
                table: "TaskHistories",
                newName: "OldTaskInfoId");

            migrationBuilder.RenameColumn(
                name: "NewColumnId",
                table: "TaskHistories",
                newName: "NewTaskInfoId");

            migrationBuilder.AlterColumn<int>(
                name: "MoveById",
                table: "TaskHistories",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "MoveAt",
                table: "TaskHistories",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskHistories_NewTaskInfoId",
                table: "TaskHistories",
                column: "NewTaskInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskHistories_OldTaskInfoId",
                table: "TaskHistories",
                column: "OldTaskInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskHistories_Tasks_NewTaskInfoId",
                table: "TaskHistories",
                column: "NewTaskInfoId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskHistories_Tasks_OldTaskInfoId",
                table: "TaskHistories",
                column: "OldTaskInfoId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskHistories_Users_MoveById",
                table: "TaskHistories",
                column: "MoveById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
