using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WATask.Migrations
{
    /// <inheritdoc />
    public partial class fixCategori : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "CategoryToProduct",
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
                name: "CategoryToProduct",
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
                name: "CategoryToProduct",
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

            migrationBuilder.AddForeignKey(
                name: "CategoryToProduct",
                table: "Product",
                column: "Id",
                principalTable: "CategoryProduct",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
