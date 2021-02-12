using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeGenOutput.API.DAL.Migrations
{
    public partial class OneToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "Contacts",
                type: "uniqueidentifier",
                nullable: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Accounts_AccountId",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_AccountId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Contacts");
        }
    }
}
