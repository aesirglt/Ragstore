using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Totten.Solution.Ragstore.Infra.Data.Migrations.RagnaStore
{
    /// <inheritdoc />
    public partial class First : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Callbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Server = table.Column<string>(type: "text", nullable: false),
                    CallbackOwnerId = table.Column<string>(type: "text", nullable: false),
                    UserCellphone = table.Column<string>(type: "text", nullable: false),
                    ItemId = table.Column<int>(type: "integer", nullable: false),
                    ItemPrice = table.Column<double>(type: "double precision", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    StoreType = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Callbacks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CallbacksSchedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Sended = table.Column<bool>(type: "boolean", nullable: false),
                    Contact = table.Column<string>(type: "text", nullable: false),
                    Body = table.Column<string>(type: "text", nullable: false),
                    SendIn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallbacksSchedule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Servers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SiteUrl = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UpdateTimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpdateTimes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Callbacks",
                columns: new[] { "Id", "CallbackOwnerId", "CreatedAt", "ItemId", "ItemPrice", "Level", "Name", "Server", "StoreType", "UpdatedAt", "UserCellphone" },
                values: new object[] { 1, "d7aeb595-44a5-4f5d-822e-980f35ace12d", new DateTime(2025, 1, 24, 9, 19, 36, 537, DateTimeKind.Utc).AddTicks(1790), 490037, 500000000.0, 4, "CallbackObscuro", "broTHOR", 2, new DateTime(2025, 1, 24, 9, 19, 36, 537, DateTimeKind.Utc).AddTicks(1791), "+5584988633251" });

            migrationBuilder.InsertData(
                table: "Servers",
                columns: new[] { "Id", "CreatedAt", "IsActive", "Name", "SiteUrl", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 24, 9, 19, 36, 537, DateTimeKind.Utc).AddTicks(92), false, "broTHOR", "https://playragnarokonlinebr.com", new DateTime(2025, 1, 24, 9, 19, 36, 537, DateTimeKind.Utc).AddTicks(93) },
                    { 2, new DateTime(2025, 1, 24, 9, 19, 36, 537, DateTimeKind.Utc).AddTicks(95), false, "broVALHALLA", "https://playragnarokonlinebr.com", new DateTime(2025, 1, 24, 9, 19, 36, 537, DateTimeKind.Utc).AddTicks(96) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Callbacks");

            migrationBuilder.DropTable(
                name: "CallbacksSchedule");

            migrationBuilder.DropTable(
                name: "Servers");

            migrationBuilder.DropTable(
                name: "UpdateTimes");
        }
    }
}
