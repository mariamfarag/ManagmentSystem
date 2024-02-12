using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddingRelationOneToMUserTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_task_tasksId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_tasksId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "taskId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "tasksId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "task",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "task",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_task_userId",
                table: "task",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_task_AspNetUsers_userId",
                table: "task",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_task_AspNetUsers_userId",
                table: "task");

            migrationBuilder.DropIndex(
                name: "IX_task_userId",
                table: "task");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "task");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "task");

            migrationBuilder.AddColumn<int>(
                name: "taskId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "tasksId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_tasksId",
                table: "AspNetUsers",
                column: "tasksId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_task_tasksId",
                table: "AspNetUsers",
                column: "tasksId",
                principalTable: "task",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
