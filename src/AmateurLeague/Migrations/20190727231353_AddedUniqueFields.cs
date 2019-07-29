using Microsoft.EntityFrameworkCore.Migrations;

namespace AmateurLeague.Migrations
{
    public partial class AddedUniqueFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Teams",
                newName: "TeamName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Sports",
                newName: "SportName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Leagues",
                newName: "LeagueName");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_TeamName",
                table: "Teams",
                column: "TeamName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Leagues_LeagueName",
                table: "Leagues",
                column: "LeagueName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Teams_TeamName",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Leagues_LeagueName",
                table: "Leagues");

            migrationBuilder.RenameColumn(
                name: "TeamName",
                table: "Teams",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "SportName",
                table: "Sports",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "LeagueName",
                table: "Leagues",
                newName: "Name");
        }
    }
}
