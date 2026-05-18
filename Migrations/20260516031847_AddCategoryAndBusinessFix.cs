using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace YellowBook.API.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryAndBusinessFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Website = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Description", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 5, 16, 3, 18, 45, 424, DateTimeKind.Utc).AddTicks(71), "Medical facilities and healthcare services", "Hospitals" },
                    { 2, new DateTime(2026, 5, 16, 3, 18, 45, 424, DateTimeKind.Utc).AddTicks(830), "Accommodation and hospitality services", "Hotels" },
                    { 3, new DateTime(2026, 5, 16, 3, 18, 45, 424, DateTimeKind.Utc).AddTicks(833), "Educational institutions", "Schools" },
                    { 4, new DateTime(2026, 5, 16, 3, 18, 45, 424, DateTimeKind.Utc).AddTicks(835), "Internet and communication services", "Internet Providers" },
                    { 5, new DateTime(2026, 5, 16, 3, 18, 45, 424, DateTimeKind.Utc).AddTicks(837), "Government offices and services", "Government Services" },
                    { 6, new DateTime(2026, 5, 16, 3, 18, 45, 424, DateTimeKind.Utc).AddTicks(839), "Food and dining establishments", "Restaurants" },
                    { 7, new DateTime(2026, 5, 16, 3, 18, 45, 424, DateTimeKind.Utc).AddTicks(841), "Various local business services", "Local Businesses" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FullName", "PasswordHash", "Role", "Username" },
                values: new object[] { 1, new DateTime(2026, 5, 16, 3, 18, 45, 764, DateTimeKind.Utc).AddTicks(6614), "admin@yellowbook.som", "System Administrator", "$2a$11$PIec4p/ZoZ4E8fmywwZ6Ret5Ys448IvfdpoynzFnwFPclYuZYXD3a", "Admin", "admin" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Address", "CategoryId", "CompanyName", "CreatedAt", "Description", "Email", "Logo", "PhoneNumber", "Website" },
                values: new object[,]
                {
                    { 1, "Mogadishu, Somalia", 1, "Mogadishu General Hospital", new DateTime(2026, 5, 16, 3, 18, 45, 426, DateTimeKind.Utc).AddTicks(1585), "Leading healthcare provider in Somalia", "info@mgh.som", "", "+252-1-123456", "https://mgh.som" },
                    { 2, "Downtown Mogadishu", 2, "Peace Hotel", new DateTime(2026, 5, 16, 3, 18, 45, 426, DateTimeKind.Utc).AddTicks(2683), "Luxury accommodation in the heart of Mogadishu", "reservations@peacehotel.som", "", "+252-1-234567", "https://peacehotel.som" },
                    { 3, "Airport Road, Mogadishu", 3, "Somali International School", new DateTime(2026, 5, 16, 3, 18, 45, 426, DateTimeKind.Utc).AddTicks(2686), "International education for Somali students", "admissions@sis.som", "", "+252-1-345678", "https://sis.som" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CategoryId",
                table: "Companies",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
