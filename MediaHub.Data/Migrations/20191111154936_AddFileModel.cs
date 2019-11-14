using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaHub.Data.Migrations
{
    public partial class AddFileModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TestModels",
                keyColumn: "Id",
                keyValue: new Guid("30ab18f6-bccc-4ced-a7aa-2faee870d5c4"));

            migrationBuilder.DeleteData(
                table: "TestModels",
                keyColumn: "Id",
                keyValue: new Guid("36e5edab-25fd-4f69-8957-c3a361857828"));

            migrationBuilder.DeleteData(
                table: "TestModels",
                keyColumn: "Id",
                keyValue: new Guid("96a217af-a13b-471a-947b-0b60a8299980"));

            migrationBuilder.DeleteData(
                table: "TestModels",
                keyColumn: "Id",
                keyValue: new Guid("98a1927e-a355-4a0a-af50-3d875fae9973"));

            migrationBuilder.CreateTable(
                name: "FileModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    FileName = table.Column<string>(nullable: false),
                    ExtensionName = table.Column<string>(nullable: false),
                    FilePath = table.Column<string>(nullable: false),
                    FileSize = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileModels", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TestModels",
                columns: new[] { "Id", "CreateDate", "TestName" },
                values: new object[,]
                {
                    { new Guid("ad6360b1-038f-44c4-8396-075ef9fd6d56"), new DateTime(2019, 11, 11, 23, 49, 35, 379, DateTimeKind.Local).AddTicks(2264), "111" },
                    { new Guid("008c115b-814d-4f18-8f21-0c74df5e4475"), new DateTime(2019, 11, 11, 23, 49, 35, 380, DateTimeKind.Local).AddTicks(5027), "222" },
                    { new Guid("3e59ef98-3eb3-4df4-a384-88e3d1125665"), new DateTime(2019, 11, 11, 23, 49, 35, 380, DateTimeKind.Local).AddTicks(5092), "333" },
                    { new Guid("91d079d8-705e-4a6a-b50d-661e711b0a0f"), new DateTime(2019, 11, 11, 23, 49, 35, 380, DateTimeKind.Local).AddTicks(5095), "444" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileModels");

            migrationBuilder.DeleteData(
                table: "TestModels",
                keyColumn: "Id",
                keyValue: new Guid("008c115b-814d-4f18-8f21-0c74df5e4475"));

            migrationBuilder.DeleteData(
                table: "TestModels",
                keyColumn: "Id",
                keyValue: new Guid("3e59ef98-3eb3-4df4-a384-88e3d1125665"));

            migrationBuilder.DeleteData(
                table: "TestModels",
                keyColumn: "Id",
                keyValue: new Guid("91d079d8-705e-4a6a-b50d-661e711b0a0f"));

            migrationBuilder.DeleteData(
                table: "TestModels",
                keyColumn: "Id",
                keyValue: new Guid("ad6360b1-038f-44c4-8396-075ef9fd6d56"));

            migrationBuilder.InsertData(
                table: "TestModels",
                columns: new[] { "Id", "CreateDate", "TestName" },
                values: new object[,]
                {
                    { new Guid("36e5edab-25fd-4f69-8957-c3a361857828"), new DateTime(2019, 11, 3, 23, 8, 57, 100, DateTimeKind.Local).AddTicks(6744), "111" },
                    { new Guid("96a217af-a13b-471a-947b-0b60a8299980"), new DateTime(2019, 11, 3, 23, 8, 57, 101, DateTimeKind.Local).AddTicks(8699), "222" },
                    { new Guid("30ab18f6-bccc-4ced-a7aa-2faee870d5c4"), new DateTime(2019, 11, 3, 23, 8, 57, 101, DateTimeKind.Local).AddTicks(8712), "333" },
                    { new Guid("98a1927e-a355-4a0a-af50-3d875fae9973"), new DateTime(2019, 11, 3, 23, 8, 57, 101, DateTimeKind.Local).AddTicks(8715), "444" }
                });
        }
    }
}
