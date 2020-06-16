using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kolokwium2s18710.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Musicians",
                columns: table => new
                {
                    IdMusician = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 30, nullable: true),
                    LastName = table.Column<string>(maxLength: 50, nullable: true),
                    NickName = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musicians", x => x.IdMusician);
                });

            migrationBuilder.CreateTable(
                name: "MusicLabels",
                columns: table => new
                {
                    IdMusicLabel = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicLabels", x => x.IdMusicLabel);
                });

            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    IdAlbum = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlbumName = table.Column<string>(maxLength: 30, nullable: true),
                    PublishDate = table.Column<DateTime>(nullable: false),
                    IdMusicLabel = table.Column<int>(nullable: false),
                    MusicLabelIdMusicLabel = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.IdAlbum);
                    table.ForeignKey(
                        name: "FK_Albums_MusicLabels_MusicLabelIdMusicLabel",
                        column: x => x.MusicLabelIdMusicLabel,
                        principalTable: "MusicLabels",
                        principalColumn: "IdMusicLabel",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tracks",
                columns: table => new
                {
                    IdTrack = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrackName = table.Column<string>(maxLength: 20, nullable: true),
                    Duration = table.Column<float>(nullable: false),
                    IdMusicAlbum = table.Column<int>(nullable: false),
                    AlbumIdAlbum = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tracks", x => x.IdTrack);
                    table.ForeignKey(
                        name: "FK_Tracks_Albums_AlbumIdAlbum",
                        column: x => x.AlbumIdAlbum,
                        principalTable: "Albums",
                        principalColumn: "IdAlbum",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MusicianTracks",
                columns: table => new
                {
                    IdTrack = table.Column<int>(nullable: false),
                    IdMusician = table.Column<int>(nullable: false),
                    IdMusicianTrack = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicianTracks", x => new { x.IdMusician, x.IdTrack });
                    table.ForeignKey(
                        name: "FK_MusicianTracks_Musicians_IdMusician",
                        column: x => x.IdMusician,
                        principalTable: "Musicians",
                        principalColumn: "IdMusician",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MusicianTracks_Tracks_IdTrack",
                        column: x => x.IdTrack,
                        principalTable: "Tracks",
                        principalColumn: "IdTrack",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Albums_MusicLabelIdMusicLabel",
                table: "Albums",
                column: "MusicLabelIdMusicLabel");

            migrationBuilder.CreateIndex(
                name: "IX_MusicianTracks_IdTrack",
                table: "MusicianTracks",
                column: "IdTrack");

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_AlbumIdAlbum",
                table: "Tracks",
                column: "AlbumIdAlbum");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MusicianTracks");

            migrationBuilder.DropTable(
                name: "Musicians");

            migrationBuilder.DropTable(
                name: "Tracks");

            migrationBuilder.DropTable(
                name: "Albums");

            migrationBuilder.DropTable(
                name: "MusicLabels");
        }
    }
}
