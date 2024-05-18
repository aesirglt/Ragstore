using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Totten.Solution.Ragstore.Infra.Data.Migrations.RagnaStore
{
    /// <inheritdoc />
    public partial class FirstRagnaStoreContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Callbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Server = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CallbackOwnerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserCellphone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    ItemPrice = table.Column<double>(type: "float", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Callbacks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Servers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    SiteUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UpdateTimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpdateTimes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Callbacks",
                columns: new[] { "Id", "CallbackOwnerId", "CreatedAt", "ItemId", "ItemPrice", "Level", "Name", "Server", "UpdatedAt", "UserCellphone" },
                values: new object[] { 1, "d7aeb595-44a5-4f5d-822e-980f35ace12d", new DateTime(2024, 5, 18, 18, 14, 6, 668, DateTimeKind.Local).AddTicks(8791), 490037, 500000000.0, 4, "CallbackObscuro", "broTHOR", new DateTime(2024, 5, 18, 18, 14, 6, 668, DateTimeKind.Local).AddTicks(8794), "+5584988633251" });

            migrationBuilder.InsertData(
                table: "Servers",
                columns: new[] { "Id", "CreatedAt", "IsActive", "Name", "SiteUrl", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 5, 18, 18, 14, 6, 668, DateTimeKind.Local).AddTicks(7567), false, "broTHOR", "https://playragnarokonlinebr.com", new DateTime(2024, 5, 18, 18, 14, 6, 668, DateTimeKind.Local).AddTicks(7582) },
                    { 2, new DateTime(2024, 5, 18, 18, 14, 6, 668, DateTimeKind.Local).AddTicks(7583), false, "broVALHALLA", "https://playragnarokonlinebr.com", new DateTime(2024, 5, 18, 18, 14, 6, 668, DateTimeKind.Local).AddTicks(7584) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Callbacks");

            migrationBuilder.DropTable(
                name: "Servers");

            migrationBuilder.DropTable(
                name: "UpdateTimes");
        }
    }
}
