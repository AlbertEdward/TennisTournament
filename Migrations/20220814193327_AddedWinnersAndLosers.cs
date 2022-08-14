using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisTournament.Migrations
{
    public partial class AddedWinnersAndLosers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Looser",
                table: "Matches",
                newName: "Loser");

            migrationBuilder.RenameColumn(
                name: "Looser",
                table: "Challenges",
                newName: "Loser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Loser",
                table: "Matches",
                newName: "Looser");

            migrationBuilder.RenameColumn(
                name: "Loser",
                table: "Challenges",
                newName: "Looser");
        }
    }
}
