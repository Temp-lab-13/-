using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TestBD3.Migrations
{
    /// <inheritdoc />
    public partial class GenderAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GenderId",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    genderId = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.genderId);
                });

            migrationBuilder.InsertData(
                table: "Genders",
                columns: new[] { "genderId", "name" },
                values: new object[,]
                {
                    { 0, "Male" },
                    { 1, "Female" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_GenderId",
                table: "users",
                column: "GenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_users_Genders_GenderId",
                table: "users",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "genderId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_Genders_GenderId",
                table: "users");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropIndex(
                name: "IX_users_GenderId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "GenderId",
                table: "users");
        }
    }
}
