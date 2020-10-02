using Microsoft.EntityFrameworkCore.Migrations;

namespace FDS2.Data.Migrations
{
    public partial class Removedcountryrestrictions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryRestrictions",
                table: "Updates");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CountryRestrictions",
                table: "Updates",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
