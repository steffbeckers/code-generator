using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Test.API.Migrations
{
    public partial class RemovedFieldsForUserTemply : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "ModifiedByUserId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "ModifiedByUserId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "ModifiedByUserId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Calls");

            migrationBuilder.DropColumn(
                name: "ModifiedByUserId",
                table: "Calls");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Calls");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "ModifiedByUserId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Accounts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserId",
                table: "Notes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedByUserId",
                table: "Notes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "Notes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserId",
                table: "Documents",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedByUserId",
                table: "Documents",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "Documents",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserId",
                table: "Contacts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedByUserId",
                table: "Contacts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "Contacts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserId",
                table: "Calls",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedByUserId",
                table: "Calls",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "Calls",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserId",
                table: "Accounts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedByUserId",
                table: "Accounts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "Accounts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
