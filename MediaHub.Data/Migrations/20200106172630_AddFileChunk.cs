using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaHub.Data.Migrations
{
    public partial class AddFileChunk : Migration
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileChunks");
        }
    }
}
