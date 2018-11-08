using Microsoft.EntityFrameworkCore.Migrations;

namespace SystemSupportingMSE.Migrations
{
    public partial class ApplyIsActiveToUserTokens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "UserTokens",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "UserTokens");
        }
    }
}
