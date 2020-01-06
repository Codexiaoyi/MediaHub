using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaHub.Data.Migrations
{
    public partial class TestTEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileModels");

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    CreateDate = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: false),
                    ExtensionName = table.Column<string>(nullable: false),
                    FilePath = table.Column<string>(nullable: false),
                    FileSize = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.CreateTable(
                name: "FileModels",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    CreateDate = table.Column<string>(nullable: true),
                    ExtensionName = table.Column<string>(nullable: false),
                    FileName = table.Column<string>(nullable: false),
                    FilePath = table.Column<string>(nullable: false),
                    FileSize = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileModels", x => x.Id);
                });
        }
    }
}
