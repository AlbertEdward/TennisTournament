using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisTournament.Migrations
{
    public partial class ChallengeModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChallengePlayer_Players_PlayerId",
                table: "ChallengePlayer");

            migrationBuilder.RenameColumn(
                name: "PlayerId",
                table: "ChallengePlayer",
                newName: "PlayersId");

            migrationBuilder.RenameIndex(
                name: "IX_ChallengePlayer_PlayerId",
                table: "ChallengePlayer",
                newName: "IX_ChallengePlayer_PlayersId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChallengePlayer_Players_PlayersId",
                table: "ChallengePlayer",
                column: "PlayersId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChallengePlayer_Players_PlayersId",
                table: "ChallengePlayer");

            migrationBuilder.RenameColumn(
                name: "PlayersId",
                table: "ChallengePlayer",
                newName: "PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_ChallengePlayer_PlayersId",
                table: "ChallengePlayer",
                newName: "IX_ChallengePlayer_PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChallengePlayer_Players_PlayerId",
                table: "ChallengePlayer",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
