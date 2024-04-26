using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Totten.Solution.Ragstore.Infra.Data.Migrations.RagnaStore
{
    /// <inheritdoc />
    public partial class CreateDbRagnaStore : Migration
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
                    Items = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    SiteUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Callbacks",
                columns: new[] { "Id", "CallbackOwnerId", "CreatedAt", "Items", "Level", "Name", "Server", "UpdatedAt", "UserCellphone" },
                values: new object[] { 1, "d7aeb595-44a5-4f5d-822e-980f35ace12d", new DateTime(2024, 4, 25, 22, 7, 2, 332, DateTimeKind.Local).AddTicks(8602), "490037:500000000", 4, "MasterCallback", "bro-THOR", new DateTime(2024, 4, 25, 22, 7, 2, 332, DateTimeKind.Local).AddTicks(8619), "+5584988633251" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Callbacks");

            migrationBuilder.DropTable(
                name: "Servers");
        }
    }
}
