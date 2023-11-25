using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodBarAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Product_AddUniqueForBarcode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Products_Barcode",
                table: "Products",
                column: "Barcode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_Barcode",
                table: "Products");
        }
    }
}
