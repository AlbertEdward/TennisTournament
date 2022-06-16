using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisTournament.Migrations
{
    public partial class UpdatedPhotos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CoverImage",
                table: "Tournaments",
                newName: "CoverPhoto");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePhoto",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePhoto",
                table: "Players");

            migrationBuilder.RenameColumn(
                name: "CoverPhoto",
                table: "Tournaments",
                newName: "CoverImage");
        }
    }
}
