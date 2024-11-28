using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookingSystem.Migrations
{
    /// <inheritdoc />
    public partial class Migration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Credits = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(6,3)", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PackageId = table.Column<int>(type: "int", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Packages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "Packages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Packages",
                columns: new[] { "Id", "Country", "Credits", "ExpiryDate", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Myanmar", 12345, new DateTime(2024, 11, 27, 15, 28, 18, 408, DateTimeKind.Local).AddTicks(8912), "package1", 12345.123m },
                    { 2, "Myanmar", 12345, new DateTime(2024, 11, 27, 15, 28, 18, 410, DateTimeKind.Local).AddTicks(7102), "package2", 123456.123m },
                    { 3, "Singapore", 12345, new DateTime(2024, 11, 27, 15, 28, 18, 410, DateTimeKind.Local).AddTicks(7130), "package3", 123456.123m },
                    { 4, "Singapore", 12345, new DateTime(2024, 11, 27, 15, 28, 18, 410, DateTimeKind.Local).AddTicks(7136), "package4", 123456.123m }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Country", "Email", "PasswordHash", "Username" },
                values: new object[,]
                {
                    { 1, "Singapore", "ccz@gmail.com", "$2a$11$2ZAml/2dIg45uH3UkXNFKu9Fvl/PUtSB/t0TT0ecj3/D6ARx/352e", "user1" },
                    { 2, "Singapore", "ccz2@gmail.com", "$2a$11$2ZAml/2dIg45uH3UkXNFKu9Fvl/PUtSB/t0TT0ecj3/D6ARx/352e", "user2" },
                    { 3, "Myanmar", "ccz@3gmail.com", "$2a$11$2ZAml/2dIg45uH3UkXNFKu9Fvl/PUtSB/t0TT0ecj3/D6ARx/352e", "user3" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_PackageId",
                table: "Bookings",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Packages");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
