using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Totten.Solution.Ragstore.Infra.Data.Migrations.RagnaStore
{
    /// <inheritdoc />
    public partial class CallbackServerIdFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Callbacks_Servers_ServerId1",
                table: "Callbacks");

            migrationBuilder.DropIndex(
                name: "IX_Callbacks_ServerId1",
                table: "Callbacks");

            migrationBuilder.DropColumn(
                name: "ServerId1",
                table: "Callbacks");

            migrationBuilder.UpdateData(
                table: "Callbacks",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 7, 8, 58, 52, 408, DateTimeKind.Utc).AddTicks(4452), new DateTime(2025, 5, 7, 8, 58, 52, 408, DateTimeKind.Utc).AddTicks(4453) });

            migrationBuilder.UpdateData(
                table: "Servers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 7, 8, 58, 52, 407, DateTimeKind.Utc).AddTicks(8141), new DateTime(2025, 5, 7, 8, 58, 52, 407, DateTimeKind.Utc).AddTicks(8143) });

            migrationBuilder.UpdateData(
                table: "Servers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 7, 8, 58, 52, 407, DateTimeKind.Utc).AddTicks(8146), new DateTime(2025, 5, 7, 8, 58, 52, 407, DateTimeKind.Utc).AddTicks(8146) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServerId1",
                table: "Callbacks",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Callbacks",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ServerId1", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 7, 7, 46, 18, 114, DateTimeKind.Utc).AddTicks(1528), null, new DateTime(2025, 5, 7, 7, 46, 18, 114, DateTimeKind.Utc).AddTicks(1532) });

            migrationBuilder.UpdateData(
                table: "Servers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 7, 7, 46, 18, 113, DateTimeKind.Utc).AddTicks(8907), new DateTime(2025, 5, 7, 7, 46, 18, 113, DateTimeKind.Utc).AddTicks(8909) });

            migrationBuilder.UpdateData(
                table: "Servers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 7, 7, 46, 18, 113, DateTimeKind.Utc).AddTicks(8913), new DateTime(2025, 5, 7, 7, 46, 18, 113, DateTimeKind.Utc).AddTicks(8913) });

            migrationBuilder.CreateIndex(
                name: "IX_Callbacks_ServerId1",
                table: "Callbacks",
                column: "ServerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Callbacks_Servers_ServerId1",
                table: "Callbacks",
                column: "ServerId1",
                principalTable: "Servers",
                principalColumn: "Id");
        }
    }
}
