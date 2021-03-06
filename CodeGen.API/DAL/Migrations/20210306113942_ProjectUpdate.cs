using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeGen.API.DAL.Migrations
{
    public partial class ProjectUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConfigJson",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfigJson",
                table: "Projects");
        }
    }
}
