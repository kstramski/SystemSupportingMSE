using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SystemSupportingMSE.Migrations
{
    public partial class AddStageAndGroupToUsersCompetitions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "GroupId",
                table: "UsersCompetitions",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "StageId",
                table: "UsersCompetitions",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Stages",
                columns: table => new
                {
                    Id = table.Column<byte>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stages", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersCompetitions_StageId",
                table: "UsersCompetitions",
                column: "StageId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersCompetitions_Stages_StageId",
                table: "UsersCompetitions",
                column: "StageId",
                principalTable: "Stages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersCompetitions_Stages_StageId",
                table: "UsersCompetitions");

            migrationBuilder.DropTable(
                name: "Stages");

            migrationBuilder.DropIndex(
                name: "IX_UsersCompetitions_StageId",
                table: "UsersCompetitions");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "UsersCompetitions");

            migrationBuilder.DropColumn(
                name: "StageId",
                table: "UsersCompetitions");
        }
    }
}
