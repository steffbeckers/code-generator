using Microsoft.EntityFrameworkCore.Migrations;

namespace Test.API.Migrations
{
    public partial class UserId2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Users_CreatedByUserId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Users_ModifiedByUserId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductDetails_Users_CreatedByUserId",
                table: "ProductDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductDetails_Users_ModifiedByUserId",
                table: "ProductDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_CreatedByUserId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_ModifiedByUserId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSupplier_Users_CreatedByUserId",
                table: "ProductSupplier");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSupplier_Users_ModifiedByUserId",
                table: "ProductSupplier");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Users_CreatedByUserId",
                table: "Suppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Users_ModifiedByUserId",
                table: "Suppliers");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Users_CreatedByUserId",
                table: "Accounts",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Users_ModifiedByUserId",
                table: "Accounts",
                column: "ModifiedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDetails_Users_CreatedByUserId",
                table: "ProductDetails",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDetails_Users_ModifiedByUserId",
                table: "ProductDetails",
                column: "ModifiedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_CreatedByUserId",
                table: "Products",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_ModifiedByUserId",
                table: "Products",
                column: "ModifiedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSupplier_Users_CreatedByUserId",
                table: "ProductSupplier",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSupplier_Users_ModifiedByUserId",
                table: "ProductSupplier",
                column: "ModifiedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Users_CreatedByUserId",
                table: "Suppliers",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Users_ModifiedByUserId",
                table: "Suppliers",
                column: "ModifiedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Users_CreatedByUserId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Users_ModifiedByUserId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductDetails_Users_CreatedByUserId",
                table: "ProductDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductDetails_Users_ModifiedByUserId",
                table: "ProductDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_CreatedByUserId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_ModifiedByUserId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSupplier_Users_CreatedByUserId",
                table: "ProductSupplier");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSupplier_Users_ModifiedByUserId",
                table: "ProductSupplier");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Users_CreatedByUserId",
                table: "Suppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Users_ModifiedByUserId",
                table: "Suppliers");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Users_CreatedByUserId",
                table: "Accounts",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Users_ModifiedByUserId",
                table: "Accounts",
                column: "ModifiedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDetails_Users_CreatedByUserId",
                table: "ProductDetails",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDetails_Users_ModifiedByUserId",
                table: "ProductDetails",
                column: "ModifiedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_CreatedByUserId",
                table: "Products",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_ModifiedByUserId",
                table: "Products",
                column: "ModifiedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSupplier_Users_CreatedByUserId",
                table: "ProductSupplier",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSupplier_Users_ModifiedByUserId",
                table: "ProductSupplier",
                column: "ModifiedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Users_CreatedByUserId",
                table: "Suppliers",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Users_ModifiedByUserId",
                table: "Suppliers",
                column: "ModifiedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
