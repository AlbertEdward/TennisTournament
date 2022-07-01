using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisTournament.Migrations
{
    public partial class Players : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tournaments_Players_PlayerId",
                table: "Tournaments");

            migrationBuilder.DropIndex(
                name: "IX_Tournaments_PlayerId",
                table: "Tournaments");

            migrationBuilder.AddColumn<int>(
                name: "TournamentId",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PlayerTournament",
                columns: table => new
                {
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    TournamentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerTournament", x => new { x.PlayerId, x.TournamentsId });
                    table.ForeignKey(
                        name: "FK_PlayerTournament_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerTournament_Tournaments_TournamentsId",
                        column: x => x.TournamentsId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerTournament_TournamentsId",
                table: "PlayerTournament",
                column: "TournamentsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerTournament");

            migrationBuilder.DropColumn(
                name: "TournamentId",
                table: "Players");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_PlayerId",
                table: "Tournaments",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tournaments_Players_PlayerId",
                table: "Tournaments",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
