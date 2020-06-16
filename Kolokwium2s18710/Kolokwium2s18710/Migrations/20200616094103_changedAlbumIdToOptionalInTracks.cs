using Microsoft.EntityFrameworkCore.Migrations;

namespace Kolokwium2s18710.Migrations
{
    public partial class changedAlbumIdToOptionalInTracks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IdMusicAlbum",
                table: "Tracks",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IdMusicAlbum",
                table: "Tracks",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
