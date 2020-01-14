using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaHub.Data.Migrations
{
    public partial class ModifyUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "MediaHubUsers",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "MediaHubUsers",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserAccount",
                table: "MediaHubUsers",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "MediaHubUsers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
