using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaHub.Data.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileChunks",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    CreateDate = table.Column<string>(nullable: true),
                    ChunkNumber = table.Column<int>(nullable: false),
                    ChunkSize = table.Column<long>(nullable: false),
                    CurrentChunkSize = table.Column<long>(nullable: false),
                    TotalSize = table.Column<long>(nullable: false),
                    Identifier = table.Column<string>(nullable: false),
                    Filename = table.Column<string>(nullable: false),
                    RelativePath = table.Column<string>(nullable: false),
                    TotalChunks = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileChunks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    CreateDate = table.Column<string>(nullable: true),
                    UserAccount = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    CreateDate = table.Column<string>(nullable: true),
                    CoverMessage = table.Column<string>(nullable: true),
                    CoverUrl = table.Column<string>(nullable: true),
                    UserId = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Albums_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    CreateDate = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    Identifier = table.Column<string>(nullable: true),
                    ExtensionName = table.Column<string>(nullable: true),
                    FileType = table.Column<int>(nullable: false),
                    FilePath = table.Column<string>(nullable: true),
                    FileSize = table.Column<long>(nullable: false),
                    AlbumId = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Files_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Albums_UserId",
                table: "Albums",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_AlbumId",
                table: "Files",
                column: "AlbumId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileChunks");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "Albums");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
