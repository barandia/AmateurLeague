using Microsoft.EntityFrameworkCore.Migrations;

namespace AmateurLeague.Migrations
{
    public partial class AddOneToManyRelLeagueSport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sports_Leagues_Name",
                table: "Sports");

            migrationBuilder.DropIndex(
                name: "IX_Sports_Name",
                table: "Sports");

            migrationBuilder.AddColumn<string>(
                name: "SportId",
                table: "Leagues",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Leagues_SportId",
                table: "Leagues",
                column: "SportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leagues_Sports_SportId",
                table: "Leagues",
                column: "SportId",
                principalTable: "Sports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leagues_Sports_SportId",
                table: "Leagues");

            migrationBuilder.DropIndex(
                name: "IX_Leagues_SportId",
                table: "Leagues");

            migrationBuilder.DropColumn(
                name: "SportId",
                table: "Leagues");

            migrationBuilder.CreateIndex(
                name: "IX_Sports_Name",
                table: "Sports",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Sports_Leagues_Name",
                table: "Sports",
                column: "Name",
                principalTable: "Leagues",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
