using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WATask2.Migrations
{
    /// <inheritdoc />
    public partial class fixId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "CategoryProduct",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "StorageId",
                table: "Product");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Product",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoriId",
                table: "Product",
                column: "CategoriId");

            migrationBuilder.AddForeignKey(
                name: "CategoryProduct",
                table: "Product",
                column: "CategoriId",
                principalTable: "CategoryProduct",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "CategoryProduct",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_CategoriId",
                table: "Product");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Product",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "StorageId",
                table: "Product",
                type: "integer",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "CategoryProduct",
                table: "Product",
                column: "Id",
                principalTable: "CategoryProduct",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
