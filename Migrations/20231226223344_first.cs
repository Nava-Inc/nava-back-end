using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nava.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "musics",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArtistId = table.Column<int>(type: "int", nullable: false),
                    NumberOfPlays = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_musics", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "userInfos",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountType = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userInfos", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "downloadedMusics",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MusicID = table.Column<int>(type: "int", nullable: false),
                    UserInfoID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_downloadedMusics", x => x.ID);
                    table.ForeignKey(
                        name: "FK_downloadedMusics_musics_MusicID",
                        column: x => x.MusicID,
                        principalTable: "musics",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_downloadedMusics_userInfos_UserInfoID",
                        column: x => x.UserInfoID,
                        principalTable: "userInfos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "playLists",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OwnerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_playLists", x => x.ID);
                    table.ForeignKey(
                        name: "FK_playLists_userInfos_OwnerID",
                        column: x => x.OwnerID,
                        principalTable: "userInfos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userInteractions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsLiked = table.Column<bool>(type: "bit", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    musicID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userInteractions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_userInteractions_musics_musicID",
                        column: x => x.musicID,
                        principalTable: "musics",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userInteractions_userInfos_UserID",
                        column: x => x.UserID,
                        principalTable: "userInfos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "playListMusics",
                columns: table => new
                {
                    MusicId = table.Column<int>(type: "int", nullable: false),
                    PlayListId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_playListMusics", x => new { x.PlayListId, x.MusicId });
                    table.ForeignKey(
                        name: "FK_playListMusics_musics_MusicId",
                        column: x => x.MusicId,
                        principalTable: "musics",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_playListMusics_playLists_PlayListId",
                        column: x => x.PlayListId,
                        principalTable: "playLists",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommentedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserInteractionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_comments_userInteractions_UserInteractionID",
                        column: x => x.UserInteractionID,
                        principalTable: "userInteractions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_comments_UserInteractionID",
                table: "comments",
                column: "UserInteractionID");

            migrationBuilder.CreateIndex(
                name: "IX_downloadedMusics_MusicID",
                table: "downloadedMusics",
                column: "MusicID");

            migrationBuilder.CreateIndex(
                name: "IX_downloadedMusics_UserInfoID",
                table: "downloadedMusics",
                column: "UserInfoID");

            migrationBuilder.CreateIndex(
                name: "IX_playListMusics_MusicId",
                table: "playListMusics",
                column: "MusicId");

            migrationBuilder.CreateIndex(
                name: "IX_playLists_OwnerID",
                table: "playLists",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_userInteractions_musicID",
                table: "userInteractions",
                column: "musicID");

            migrationBuilder.CreateIndex(
                name: "IX_userInteractions_UserID",
                table: "userInteractions",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comments");

            migrationBuilder.DropTable(
                name: "downloadedMusics");

            migrationBuilder.DropTable(
                name: "playListMusics");

            migrationBuilder.DropTable(
                name: "userInteractions");

            migrationBuilder.DropTable(
                name: "playLists");

            migrationBuilder.DropTable(
                name: "musics");

            migrationBuilder.DropTable(
                name: "userInfos");
        }
    }
}
