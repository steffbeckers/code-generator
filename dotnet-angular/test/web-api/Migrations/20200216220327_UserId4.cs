using Microsoft.EntityFrameworkCore.Migrations;

namespace Test.API.Migrations
{
    public partial class UserId4 : Migration
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

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_CreatedByUserId",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_ModifiedByUserId",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_ProductSupplier_CreatedByUserId",
                table: "ProductSupplier");

            migrationBuilder.DropIndex(
                name: "IX_ProductSupplier_ModifiedByUserId",
                table: "ProductSupplier");

            migrationBuilder.DropIndex(
                name: "IX_Products_CreatedByUserId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ModifiedByUserId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_ProductDetails_CreatedByUserId",
                table: "ProductDetails");

            migrationBuilder.DropIndex(
                name: "IX_ProductDetails_ModifiedByUserId",
                table: "ProductDetails");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_CreatedByUserId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_ModifiedByUserId",
                table: "Accounts");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_CreatedByUserId",
                table: "Suppliers",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_ModifiedByUserId",
                table: "Suppliers",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSupplier_CreatedByUserId",
                table: "ProductSupplier",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSupplier_ModifiedByUserId",
                table: "ProductSupplier",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CreatedByUserId",
                table: "Products",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ModifiedByUserId",
                table: "Products",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetails_CreatedByUserId",
                table: "ProductDetails",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetails_ModifiedByUserId",
                table: "ProductDetails",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CreatedByUserId",
                table: "Accounts",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ModifiedByUserId",
                table: "Accounts",
                column: "ModifiedByUserId");

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

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_CreatedByUserId",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_ModifiedByUserId",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_ProductSupplier_CreatedByUserId",
                table: "ProductSupplier");

            migrationBuilder.DropIndex(
                name: "IX_ProductSupplier_ModifiedByUserId",
                table: "ProductSupplier");

            migrationBuilder.DropIndex(
                name: "IX_Products_CreatedByUserId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ModifiedByUserId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_ProductDetails_CreatedByUserId",
                table: "ProductDetails");

            migrationBuilder.DropIndex(
                name: "IX_ProductDetails_ModifiedByUserId",
                table: "ProductDetails");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_CreatedByUserId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_ModifiedByUserId",
                table: "Accounts");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_CreatedByUserId",
                table: "Suppliers",
                column: "CreatedByUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_ModifiedByUserId",
                table: "Suppliers",
                column: "ModifiedByUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductSupplier_CreatedByUserId",
                table: "ProductSupplier",
                column: "CreatedByUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductSupplier_ModifiedByUserId",
                table: "ProductSupplier",
                column: "ModifiedByUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CreatedByUserId",
                table: "Products",
                column: "CreatedByUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ModifiedByUserId",
                table: "Products",
                column: "ModifiedByUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetails_CreatedByUserId",
                table: "ProductDetails",
                column: "CreatedByUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetails_ModifiedByUserId",
                table: "ProductDetails",
                column: "ModifiedByUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CreatedByUserId",
                table: "Accounts",
                column: "CreatedByUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ModifiedByUserId",
                table: "Accounts",
                column: "ModifiedByUserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Users_CreatedByUserId",
                table: "Accounts",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Users_ModifiedByUserId",
                table: "Accounts",
                column: "ModifiedByUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDetails_Users_CreatedByUserId",
                table: "ProductDetails",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDetails_Users_ModifiedByUserId",
                table: "ProductDetails",
                column: "ModifiedByUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_CreatedByUserId",
                table: "Products",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_ModifiedByUserId",
                table: "Products",
                column: "ModifiedByUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSupplier_Users_CreatedByUserId",
                table: "ProductSupplier",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSupplier_Users_ModifiedByUserId",
                table: "ProductSupplier",
                column: "ModifiedByUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Users_CreatedByUserId",
                table: "Suppliers",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Users_ModifiedByUserId",
                table: "Suppliers",
                column: "ModifiedByUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
