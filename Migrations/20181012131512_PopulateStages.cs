using Microsoft.EntityFrameworkCore.Migrations;

namespace SystemSupportingMSE.Migrations
{
    public partial class PopulateStages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Stages(Name) VALUES ('1')");
            migrationBuilder.Sql("INSERT INTO Stages(Name) VALUES ('2')");
            migrationBuilder.Sql("INSERT INTO Stages(Name) VALUES ('3')");
            migrationBuilder.Sql("INSERT INTO Stages(Name) VALUES ('4')");
            migrationBuilder.Sql("INSERT INTO Stages(Name) VALUES ('5')");
            migrationBuilder.Sql("INSERT INTO Stages(Name) VALUES ('16th-finals')");
            migrationBuilder.Sql("INSERT INTO Stages(Name) VALUES ('Eighth-finals')");
            migrationBuilder.Sql("INSERT INTO Stages(Name) VALUES ('Quaterfinals')");
            migrationBuilder.Sql("INSERT INTO Stages(Name) VALUES ('Semifinals')");
            migrationBuilder.Sql("INSERT INTO Stages(Name) VALUES ('Final')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE * FROM Stages");
        }
    }
}
