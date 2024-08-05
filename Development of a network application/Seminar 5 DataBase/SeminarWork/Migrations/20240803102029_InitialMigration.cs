using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeminarWork.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("user_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "messages",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    messageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    messageData = table.Column<DateTime>(type: "datetime2", nullable: false),
                    is_sent = table.Column<bool>(type: "bit", nullable: false),
                    UserTOId = table.Column<int>(type: "int", nullable: false),
                    UserFromId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("messagePK", x => x.id);
                    table.ForeignKey(
                        name: "messageFromUserFK",
                        column: x => x.UserFromId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "messageToUserFK",
                        column: x => x.UserTOId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_messages_UserFromId",
                table: "messages",
                column: "UserFromId");

            migrationBuilder.CreateIndex(
                name: "IX_messages_UserTOId",
                table: "messages",
                column: "UserTOId");

            migrationBuilder.CreateIndex(
                name: "IX_users_FullName",
                table: "users",
                column: "FullName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "messages");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
