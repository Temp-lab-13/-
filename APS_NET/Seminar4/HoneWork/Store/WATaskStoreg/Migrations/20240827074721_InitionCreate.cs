using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WATaskStoreg.Migrations
{
    /// <inheritdoc />
    public partial class InitionCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Storage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductID = table.Column<int>(type: "integer", maxLength: 255, nullable: false),
                    ProductCount = table.Column<int>(type: "integer", maxLength: 255, nullable: false),
                    PositionName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Descript = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PositionID", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Storage_PositionName",
                table: "Storage",
                column: "PositionName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Storage");
        }
    }
}
