using Microsoft.EntityFrameworkCore.Migrations;

namespace SystemSupportingMSE.Migrations
{
    public partial class ChangeDescriptionInRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE Roles SET Description='Administrator' WHERE Id=1");
            migrationBuilder.Sql("UPDATE Roles SET Description='Moderator' WHERE Id=2");
            migrationBuilder.Sql("UPDATE Roles SET Description='User' WHERE Id=3");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
