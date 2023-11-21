using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicApp.Data.Migrations
{
    public partial class uniqueReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Album_Band_BandID",
                table: "Album");

            migrationBuilder.DropIndex(
                name: "IX_Review_UserID",
                table: "Review");

            migrationBuilder.AlterColumn<int>(
                name: "BandID",
                table: "Album",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Review_UserID",
                table: "Review",
                column: "UserID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Album_Band_BandID",
                table: "Album",
                column: "BandID",
                principalTable: "Band",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Album_Band_BandID",
                table: "Album");

            migrationBuilder.DropIndex(
                name: "IX_Review_UserID",
                table: "Review");

            migrationBuilder.AlterColumn<int>(
                name: "BandID",
                table: "Album",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Review_UserID",
                table: "Review",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Album_Band_BandID",
                table: "Album",
                column: "BandID",
                principalTable: "Band",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
