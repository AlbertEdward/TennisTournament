using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisTournament.Migrations
{
    public partial class EditedPlayerColumnNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Wons",
                table: "Players",
                newName: "Wins");

            migrationBuilder.RenameColumn(
                name: "Losts",
                table: "Players",
                newName: "Losses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Wins",
                table: "Players",
                newName: "Wons");

            migrationBuilder.RenameColumn(
                name: "Losses",
                table: "Players",
                newName: "Losts");
        }
    }
}
