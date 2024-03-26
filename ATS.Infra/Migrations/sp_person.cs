using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ATS.Infra.Migrations
{
    public partial class sp_person : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //This migration class will create the example SP
            var sp = @"CREATE PROCEDURE [GetPersons]
            AS
            BEGIN
                select * from Person
            END";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE [GetPersons]");
        }
    }
}
