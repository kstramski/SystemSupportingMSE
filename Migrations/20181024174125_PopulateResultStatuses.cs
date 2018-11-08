using Microsoft.EntityFrameworkCore.Migrations;

namespace SystemSupportingMSE.Migrations
{
    public partial class PopulateResultStatuses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ResultStatuses",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.Sql("INSERT INTO ResultStatuses (Name) VALUES ('Win')");
            migrationBuilder.Sql("INSERT INTO ResultStatuses (Name) VALUES ('Lose')");
            migrationBuilder.Sql("INSERT INTO ResultStatuses (Name) VALUES ('Draw')");
            migrationBuilder.Sql("INSERT INTO ResultStatuses (Name) VALUES ('Qualified')");
            migrationBuilder.Sql("INSERT INTO ResultStatuses (Name) VALUES ('Disqualified')");
            migrationBuilder.Sql("INSERT INTO ResultStatuses (Name) VALUES ('Nonparticipation')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM ResultStatuses WHERE Name IN ('Win', 'Lose', 'Draw', 'Qualified', 'Disqualified', 'Nonparticipation')");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ResultStatuses",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20);
        }
    }
}
