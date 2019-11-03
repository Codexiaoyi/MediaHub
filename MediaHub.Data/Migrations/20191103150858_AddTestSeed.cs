using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaHub.Data.Migrations
{
    public partial class AddTestSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
