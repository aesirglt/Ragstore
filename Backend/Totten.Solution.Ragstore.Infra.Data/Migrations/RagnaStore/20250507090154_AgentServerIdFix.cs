using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Totten.Solution.Ragstore.Infra.Data.Migrations.RagnaStore
{
    /// <inheritdoc />
    public partial class AgentServerIdFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agents_Servers_ServerId1",
                table: "Agents");

            migrationBuilder.DropIndex(
                name: "IX_Agents_ServerId1",
                table: "Agents");

            migrationBuilder.DropColumn(
                name: "ServerId1",
                table: "Agents");

            migrationBuilder.UpdateData(
                table: "Callbacks",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 7, 9, 1, 54, 433, DateTimeKind.Utc).AddTicks(3413), new DateTime(2025, 5, 7, 9, 1, 54, 433, DateTimeKind.Utc).AddTicks(3414) });

            migrationBuilder.UpdateData(
                table: "Servers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 7, 9, 1, 54, 432, DateTimeKind.Utc).AddTicks(4777), new DateTime(2025, 5, 7, 9, 1, 54, 432, DateTimeKind.Utc).AddTicks(4779) });

            migrationBuilder.UpdateData(
                table: "Servers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 7, 9, 1, 54, 432, DateTimeKind.Utc).AddTicks(4782), new DateTime(2025, 5, 7, 9, 1, 54, 432, DateTimeKind.Utc).AddTicks(4782) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServerId1",
                table: "Agents",
                type: "integer",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Agents_ServerId1",
                table: "Agents",
                column: "ServerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Agents_Servers_ServerId1",
                table: "Agents",
                column: "ServerId1",
                principalTable: "Servers",
                principalColumn: "Id");
        }
    }
}
