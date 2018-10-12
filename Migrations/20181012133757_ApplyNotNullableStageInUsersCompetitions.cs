using Microsoft.EntityFrameworkCore.Migrations;

namespace SystemSupportingMSE.Migrations
{
    public partial class ApplyNotNullableStageInUsersCompetitions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersCompetitions_Stages_StageId",
                table: "UsersCompetitions");

            migrationBuilder.AlterColumn<byte>(
                name: "StageId",
                table: "UsersCompetitions",
                nullable: false,
                oldClrType: typeof(byte),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersCompetitions_Stages_StageId",
                table: "UsersCompetitions",
                column: "StageId",
                principalTable: "Stages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersCompetitions_Stages_StageId",
                table: "UsersCompetitions");

            migrationBuilder.AlterColumn<byte>(
                name: "StageId",
                table: "UsersCompetitions",
                nullable: true,
                oldClrType: typeof(byte));

            migrationBuilder.AddForeignKey(
                name: "FK_UsersCompetitions_Stages_StageId",
                table: "UsersCompetitions",
                column: "StageId",
                principalTable: "Stages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
