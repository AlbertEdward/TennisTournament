using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisTournament.Migrations
{
    public partial class GuestId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PlayerHostId",
                table: "Challenges",
                newName: "PlayerHostUserId");

            migrationBuilder.AlterColumn<int>(
                name: "PlayerGuestId",
                table: "Challenges",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PlayerHostUserId",
                table: "Challenges",
                newName: "PlayerHostId");

            migrationBuilder.AlterColumn<string>(
                name: "PlayerGuestId",
                table: "Challenges",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
