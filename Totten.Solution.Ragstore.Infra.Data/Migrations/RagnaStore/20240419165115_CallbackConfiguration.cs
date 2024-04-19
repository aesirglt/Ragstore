using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Totten.Solution.Ragstore.Infra.Data.Migrations.RagnaStore
{
    /// <inheritdoc />
    public partial class CallbackConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CharacterId1",
                table: "BuyingStores",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 4, 19, 13, 51, 11, 428, DateTimeKind.Local).AddTicks(5246), new DateTime(2024, 4, 19, 13, 51, 11, 428, DateTimeKind.Local).AddTicks(5262) });

            migrationBuilder.InsertData(
                table: "Callbacks",
                columns: new[] { "Id", "CallbackOwnerId", "CreatedAt", "Items", "Level", "Name", "Server", "UpdatedAt", "UserCellphone" },
                values: new object[] { 1, "d7aeb595-44a5-4f5d-822e-980f35ace12d", new DateTime(2024, 4, 19, 13, 51, 11, 372, DateTimeKind.Local).AddTicks(3801), "490037:500000000", 4, "MasterCallback", "bro-THOR", new DateTime(2024, 4, 19, 13, 51, 11, 372, DateTimeKind.Local).AddTicks(3819), "+5584988633251" });

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 4, 19, 13, 51, 11, 430, DateTimeKind.Local).AddTicks(9355), new DateTime(2024, 4, 19, 13, 51, 11, 430, DateTimeKind.Local).AddTicks(9367) });

            migrationBuilder.CreateIndex(
                name: "IX_BuyingStores_CharacterId1",
                table: "BuyingStores",
                column: "CharacterId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BuyingStores_Characters_CharacterId1",
                table: "BuyingStores",
                column: "CharacterId1",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BuyingStores_Characters_CharacterId1",
                table: "BuyingStores");

            migrationBuilder.DropTable(
                name: "Callbacks");

            migrationBuilder.DropIndex(
                name: "IX_BuyingStores_CharacterId1",
                table: "BuyingStores");

            migrationBuilder.DropColumn(
                name: "CharacterId1",
                table: "BuyingStores");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 4, 18, 22, 6, 22, 575, DateTimeKind.Local).AddTicks(8779), new DateTime(2024, 4, 18, 22, 6, 22, 575, DateTimeKind.Local).AddTicks(8811) });

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 4, 18, 22, 6, 22, 577, DateTimeKind.Local).AddTicks(2369), new DateTime(2024, 4, 18, 22, 6, 22, 577, DateTimeKind.Local).AddTicks(2383) });
        }
    }
}
