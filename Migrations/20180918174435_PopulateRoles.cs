using Microsoft.EntityFrameworkCore.Migrations;

namespace SystemSupportingMSE.Migrations
{
    public partial class PopulateRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Roles (Name) VALUES ('Administrator')");
            migrationBuilder.Sql("INSERT INTO Roles (Name) VALUES ('Moderator')");
            migrationBuilder.Sql("INSERT INTO Roles (Name) VALUES ('User')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DETELE FROM Roles WHERE Name IN ('Administrator', 'Moderator', 'User')");
        }
    }
}
