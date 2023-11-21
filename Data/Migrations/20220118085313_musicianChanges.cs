using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicApp.Data.Migrations
{
    public partial class musicianChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Musician_Album_AlbumID",
                table: "Musician");

            migrationBuilder.DropTable(
                name: "Song");

            migrationBuilder.RenameColumn(
                name: "AlbumID",
                table: "Musician",
                newName: "BandID");

            migrationBuilder.RenameIndex(
                name: "IX_Musician_AlbumID",
                table: "Musician",
                newName: "IX_Musician_BandID");

            migrationBuilder.AddForeignKey(
                name: "FK_Musician_Band_BandID",
                table: "Musician",
                column: "BandID",
                principalTable: "Band",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Musician_Band_BandID",
                table: "Musician");

            migrationBuilder.RenameColumn(
                name: "BandID",
                table: "Musician",
                newName: "AlbumID");

            migrationBuilder.RenameIndex(
                name: "IX_Musician_BandID",
                table: "Musician",
                newName: "IX_Musician_AlbumID");

            migrationBuilder.CreateTable(
                name: "Song",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlbumID = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Song", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Song_Album_AlbumID",
                        column: x => x.AlbumID,
                        principalTable: "Album",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Song_AlbumID",
                table: "Song",
                column: "AlbumID");

            migrationBuilder.AddForeignKey(
                name: "FK_Musician_Album_AlbumID",
                table: "Musician",
                column: "AlbumID",
                principalTable: "Album",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
