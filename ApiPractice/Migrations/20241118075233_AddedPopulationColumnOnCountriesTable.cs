using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiPractice.Migrations
{
    public partial class AddedPopulationColumnOnCountriesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Population",
                table: "Countries",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Population",
                table: "Countries");
        }
    }
}
