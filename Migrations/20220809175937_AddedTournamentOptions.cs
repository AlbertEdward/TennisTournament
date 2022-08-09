using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisTournament.Migrations
{
    public partial class AddedTournamentOptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sets",
                table: "Tournaments",
                newName: "TournamentType");

            migrationBuilder.RenameColumn(
                name: "Rules",
                table: "Tournaments",
                newName: "Set");

            migrationBuilder.RenameColumn(
                name: "LastSets",
                table: "Tournaments",
                newName: "Rule");

            migrationBuilder.RenameColumn(
                name: "Games",
                table: "Tournaments",
                newName: "LastSet");

            migrationBuilder.AddColumn<int>(
                name: "Game",
                table: "Tournaments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Tournaments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Game",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Tournaments");

            migrationBuilder.RenameColumn(
                name: "TournamentType",
                table: "Tournaments",
                newName: "Sets");

            migrationBuilder.RenameColumn(
                name: "Set",
                table: "Tournaments",
                newName: "Rules");

            migrationBuilder.RenameColumn(
                name: "Rule",
                table: "Tournaments",
                newName: "LastSets");

            migrationBuilder.RenameColumn(
                name: "LastSet",
                table: "Tournaments",
                newName: "Games");
        }
    }
}
