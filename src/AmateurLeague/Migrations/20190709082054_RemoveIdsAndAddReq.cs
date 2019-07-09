using Microsoft.EntityFrameworkCore.Migrations;

namespace AmateurLeague.Migrations
{
    public partial class RemoveIdsAndAddReq : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leagues_Sports_SportId",
                table: "Leagues");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Leagues_LeagueName",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Players");

            migrationBuilder.AlterColumn<string>(
                name: "LeagueName",
                table: "Teams",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SportId",
                table: "Leagues",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Leagues_Sports_SportId",
                table: "Leagues",
                column: "SportId",
                principalTable: "Sports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Leagues_LeagueName",
                table: "Teams",
                column: "LeagueName",
                principalTable: "Leagues",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leagues_Sports_SportId",
                table: "Leagues");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Leagues_LeagueName",
                table: "Teams");

            migrationBuilder.AlterColumn<string>(
                name: "LeagueName",
                table: "Teams",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Players",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SportId",
                table: "Leagues",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_Leagues_Sports_SportId",
                table: "Leagues",
                column: "SportId",
                principalTable: "Sports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Leagues_LeagueName",
                table: "Teams",
                column: "LeagueName",
                principalTable: "Leagues",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
