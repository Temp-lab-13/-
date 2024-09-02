using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WATask.Migrations
{
    /// <inheritdoc />
    public partial class fixCataloge2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "CategoryToProduct",
                table: "Product");

            migrationBuilder.AddForeignKey(
                name: "CategoryProduct",
                table: "Product",
                column: "CategoriId",
                principalTable: "CategoryProduct",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "CategoryProduct",
                table: "Product");

            migrationBuilder.AddForeignKey(
                name: "CategoryToProduct",
                table: "Product",
                column: "CategoriId",
                principalTable: "CategoryProduct",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
