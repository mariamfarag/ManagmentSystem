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

            migrationBuilder.RenameColumn(
                name: "tasksId",
                table: "AspNetUsers",
                newName: "UserType");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "task",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PriorityLevel",
                table: "task",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "task",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "task",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_task_userId",
                table: "task",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_task_AspNetUsers_userId",
                table: "task",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
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

            migrationBuilder.RenameColumn(
                name: "UserType",
                table: "AspNetUsers",
                newName: "tasksId");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "task",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PriorityLevel",
                table: "task",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "task",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "taskId",
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
