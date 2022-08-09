using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisTournament.Migrations
{
    public partial class EditedPlayers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerTournament_Players_PlayerId",
                table: "PlayerTournament");

            migrationBuilder.RenameColumn(
                name: "PlayerId",
                table: "PlayerTournament",
                newName: "PlayersId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerTournament_Players_PlayersId",
                table: "PlayerTournament",
                column: "PlayersId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerTournament_Players_PlayersId",
                table: "PlayerTournament");

            migrationBuilder.RenameColumn(
                name: "PlayersId",
                table: "PlayerTournament",
                newName: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerTournament_Players_PlayerId",
                table: "PlayerTournament",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
