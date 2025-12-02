using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelLanhShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixCustomerandUserToRegister : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Suppliers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Sales",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "SaleDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "InventoryEntryDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "InventoryEntries",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Employees",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Customers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ComboTemplates",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ComboTemplateItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserId",
                table: "Customers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Users_UserId",
                table: "Customers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Users_UserId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_UserId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "SaleDetails");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "InventoryEntryDetails");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "InventoryEntries");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ComboTemplates");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ComboTemplateItems");
        }
    }
}
