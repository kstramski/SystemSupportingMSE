using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SystemSupportingMSE.Migrations
{
    public partial class AddEventsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Gender",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    RegistrationDateStart = table.Column<DateTime>(nullable: false),
                    RegistrationDateEnd = table.Column<DateTime>(nullable: false),
                    EventDateStart = table.Column<DateTime>(nullable: false),
                    EventDateEnd = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserEvent",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    EventId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEvent", x => new { x.UserId, x.EventId });
                    table.ForeignKey(
                        name: "FK_UserEvent_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserEvent_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserEvent_EventId",
                table: "UserEvent",
                column: "EventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserEvent");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Users");
        }
    }
}
