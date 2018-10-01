using Microsoft.EntityFrameworkCore.Migrations;

namespace SystemSupportingMSE.Migrations
{
    public partial class PopulateGenders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Genders (Id, Name) VALUES (1,'Male')");
            migrationBuilder.Sql("INSERT INTO Genders (Id, Name) VALUES (2, 'Female')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Genders WHERE Name IN ('Male', 'Female')");
        }
    }
}
