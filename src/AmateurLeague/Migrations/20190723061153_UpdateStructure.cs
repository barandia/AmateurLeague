using Microsoft.EntityFrameworkCore.Migrations;

namespace AmateurLeague.Migrations
{
    public partial class UpdateStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sports_Leagues_Name",
                table: "Sports");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Leagues_LeagueName",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Sports_Name",
                table: "Sports");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Sports");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Players");

            migrationBuilder.AlterColumn<string>(
                name: "LeagueName",
                table: "Teams",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GenderType",
                table: "Sports",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SportId",
                table: "Leagues",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Sports",
                columns: new[] { "Id", "GenderType", "Name" },
                values: new object[,]
                {
                    { "1", "Men", "Basketball" },
                    { "2", "Women", "Basketball" },
                    { "3", "Coed", "Basketball" },
                    { "4", "Men", "Baseball" },
                    { "5", "Women", "Baseball" },
                    { "6", "Coed", "Baseball" },
                    { "7", "Men", "Soccer" },
                    { "8", "Women", "Soccer" },
                    { "9", "Coed", "Soccer" },
                    { "10", "Men", "Flag Football" },
                    { "11", "Women", "Flag Football" },
                    { "12", "Coed", "Flag Football" }
                });

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

            migrationBuilder.DropIndex(
                name: "IX_Leagues_SportId",
                table: "Leagues");

            migrationBuilder.DeleteData(
                table: "Sports",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "Sports",
                keyColumn: "Id",
                keyValue: "10");

            migrationBuilder.DeleteData(
                table: "Sports",
                keyColumn: "Id",
                keyValue: "11");

            migrationBuilder.DeleteData(
                table: "Sports",
                keyColumn: "Id",
                keyValue: "12");

            migrationBuilder.DeleteData(
                table: "Sports",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "Sports",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "Sports",
                keyColumn: "Id",
                keyValue: "4");

            migrationBuilder.DeleteData(
                table: "Sports",
                keyColumn: "Id",
                keyValue: "5");

            migrationBuilder.DeleteData(
                table: "Sports",
                keyColumn: "Id",
                keyValue: "6");

            migrationBuilder.DeleteData(
                table: "Sports",
                keyColumn: "Id",
                keyValue: "7");

            migrationBuilder.DeleteData(
                table: "Sports",
                keyColumn: "Id",
                keyValue: "8");

            migrationBuilder.DeleteData(
                table: "Sports",
                keyColumn: "Id",
                keyValue: "9");

            migrationBuilder.DropColumn(
                name: "GenderType",
                table: "Sports");

            migrationBuilder.DropColumn(
                name: "SportId",
                table: "Leagues");

            migrationBuilder.AlterColumn<string>(
                name: "LeagueName",
                table: "Teams",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Sports",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Players",
                nullable: true);

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
