using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Test.API.Migrations
{
    public partial class OneToManyTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId",
                table: "Todoes",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "Contacts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Todoes_ProjectId",
                table: "Todoes",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_AccountId",
                table: "Contacts",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Accounts_AccountId",
                table: "Contacts",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Todoes_Projects_ProjectId",
                table: "Todoes",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Accounts_AccountId",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Todoes_Projects_ProjectId",
                table: "Todoes");

            migrationBuilder.DropIndex(
                name: "IX_Todoes_ProjectId",
                table: "Todoes");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_AccountId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Todoes");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Contacts");
        }
    }
}
