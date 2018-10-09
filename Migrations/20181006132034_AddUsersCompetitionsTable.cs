using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SystemSupportingMSE.Migrations
{
    public partial class AddUsersCompetitionsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CompetitionDate",
                table: "EventsCompetitions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TimePerGroup",
                table: "EventsCompetitions",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UsersCompetitions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    EventId = table.Column<int>(nullable: false),
                    CompetitionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersCompetitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersCompetitions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersCompetitions_EventsCompetitions_CompetitionId_EventId",
                        columns: x => new { x.CompetitionId, x.EventId },
                        principalTable: "EventsCompetitions",
                        principalColumns: new[] { "CompetitionId", "EventId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersCompetitions_UserId",
                table: "UsersCompetitions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersCompetitions_CompetitionId_EventId",
                table: "UsersCompetitions",
                columns: new[] { "CompetitionId", "EventId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersCompetitions");

            migrationBuilder.DropColumn(
                name: "CompetitionDate",
                table: "EventsCompetitions");

            migrationBuilder.DropColumn(
                name: "TimePerGroup",
                table: "EventsCompetitions");
        }
    }
}
