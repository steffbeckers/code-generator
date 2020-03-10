using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Test.API.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Users_UserId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderStates_Orders_OrderId",
                table: "OrderStates");

            migrationBuilder.DropIndex(
                name: "IX_OrderStates_OrderId",
                table: "OrderStates");

            migrationBuilder.DropIndex(
                name: "IX_Carts_UserId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "OrderStates");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Carts");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderStateId",
                table: "Orders",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderStateId",
                table: "Orders",
                column: "OrderStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderStates_OrderStateId",
                table: "Orders",
                column: "OrderStateId",
                principalTable: "OrderStates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderStates_OrderStateId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderStateId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderStateId",
                table: "Orders");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "OrderStates",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Carts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderStates_OrderId",
                table: "OrderStates",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Users_UserId",
                table: "Carts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderStates_Orders_OrderId",
                table: "OrderStates",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
