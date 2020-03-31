using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RJM.API.Migrations
{
    public partial class FieldUpdatesAndDocumentModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "ResumeSkill");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "JobSkill");

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "ResumeSkill",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "JobSkill",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    DisplayName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    URL = table.Column<string>(nullable: true),
                    MimeType = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedByUserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_Users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Documents_Users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documents_CreatedByUserId",
                table: "Documents",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ModifiedByUserId",
                table: "Documents",
                column: "ModifiedByUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "ResumeSkill");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "JobSkill");

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "ResumeSkill",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "JobSkill",
                type: "int",
                nullable: true);
        }
    }
}
