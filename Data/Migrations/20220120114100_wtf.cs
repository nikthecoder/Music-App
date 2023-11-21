using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicApp.Data.Migrations
{
    public partial class wtf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_AspNetUsers_UserId",
                table: "Review");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Review",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Review_UserId",
                table: "Review",
                newName: "IX_Review_UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_AspNetUsers_UserID",
                table: "Review",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_AspNetUsers_UserID",
                table: "Review");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Review",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Review_UserID",
                table: "Review",
                newName: "IX_Review_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_AspNetUsers_UserId",
                table: "Review",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
