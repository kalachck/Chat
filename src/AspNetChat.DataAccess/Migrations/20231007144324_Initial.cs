using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AspNetChat.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChatName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chats_Users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ChatId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserChats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ChatId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserChats_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserChats_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Name", "Surname" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 10, 7, 14, 43, 24, 768, DateTimeKind.Utc).AddTicks(9368), "Сергей", "Kalachev" },
                    { 2, new DateTime(2023, 10, 7, 14, 43, 24, 768, DateTimeKind.Utc).AddTicks(9370), "Gleb", "Ponteleev" }
                });

            migrationBuilder.InsertData(
                table: "Chats",
                columns: new[] { "Id", "ChatName", "CreatedAt", "CreatedByUserId" },
                values: new object[,]
                {
                    { 1, "Chat1", new DateTime(2023, 10, 7, 14, 43, 24, 767, DateTimeKind.Utc).AddTicks(7408), 1 },
                    { 2, "Chat2", new DateTime(2023, 10, 7, 14, 43, 24, 767, DateTimeKind.Utc).AddTicks(7410), 2 }
                });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "ChatId", "Content", "CreatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, 1, "Hello from User1 in Chat1", new DateTime(2023, 10, 7, 14, 43, 24, 768, DateTimeKind.Utc).AddTicks(2738), 1 },
                    { 2, 2, "Hello from User2 in Chat2", new DateTime(2023, 10, 7, 14, 43, 24, 768, DateTimeKind.Utc).AddTicks(2740), 2 }
                });

            migrationBuilder.InsertData(
                table: "UserChats",
                columns: new[] { "Id", "ChatId", "CreatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 10, 7, 14, 43, 24, 768, DateTimeKind.Utc).AddTicks(7153), 1 },
                    { 2, 1, new DateTime(2023, 10, 7, 14, 43, 24, 768, DateTimeKind.Utc).AddTicks(7155), 2 },
                    { 3, 2, new DateTime(2023, 10, 7, 14, 43, 24, 768, DateTimeKind.Utc).AddTicks(7156), 1 },
                    { 4, 2, new DateTime(2023, 10, 7, 14, 43, 24, 768, DateTimeKind.Utc).AddTicks(7157), 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chats_CreatedByUserId",
                table: "Chats",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatId",
                table: "Messages",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserId",
                table: "Messages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserChats_ChatId",
                table: "UserChats",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_UserChats_UserId",
                table: "UserChats",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "UserChats");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
