using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RJM.API.Migrations
{
    public partial class ResumeStateFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ResumeStateId",
                table: "Resumes",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ResumeStates",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    DisplayName = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedByUserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResumeStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResumeStates_Users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ResumeStates_Users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_ResumeStateId",
                table: "Resumes",
                column: "ResumeStateId");

            migrationBuilder.CreateIndex(
                name: "IX_ResumeStates_CreatedByUserId",
                table: "ResumeStates",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResumeStates_ModifiedByUserId",
                table: "ResumeStates",
                column: "ModifiedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Resumes_ResumeStates_ResumeStateId",
                table: "Resumes",
                column: "ResumeStateId",
                principalTable: "ResumeStates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resumes_ResumeStates_ResumeStateId",
                table: "Resumes");

            migrationBuilder.DropTable(
                name: "ResumeStates");

            migrationBuilder.DropIndex(
                name: "IX_Resumes_ResumeStateId",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "ResumeStateId",
                table: "Resumes");
        }
    }
}
