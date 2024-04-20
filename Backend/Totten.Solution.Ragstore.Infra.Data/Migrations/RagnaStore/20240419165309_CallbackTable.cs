using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Totten.Solution.Ragstore.Infra.Data.Migrations.RagnaStore
{
    /// <inheritdoc />
    public partial class CallbackTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 4, 19, 13, 53, 5, 348, DateTimeKind.Local).AddTicks(3068), new DateTime(2024, 4, 19, 13, 53, 5, 348, DateTimeKind.Local).AddTicks(3083) });

            migrationBuilder.UpdateData(
                table: "Callbacks",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 4, 19, 13, 53, 5, 279, DateTimeKind.Local).AddTicks(5876), new DateTime(2024, 4, 19, 13, 53, 5, 279, DateTimeKind.Local).AddTicks(5891) });

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 4, 19, 13, 53, 5, 350, DateTimeKind.Local).AddTicks(3828), new DateTime(2024, 4, 19, 13, 53, 5, 350, DateTimeKind.Local).AddTicks(3834) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 4, 19, 13, 51, 11, 428, DateTimeKind.Local).AddTicks(5246), new DateTime(2024, 4, 19, 13, 51, 11, 428, DateTimeKind.Local).AddTicks(5262) });

            migrationBuilder.UpdateData(
                table: "Callbacks",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 4, 19, 13, 51, 11, 372, DateTimeKind.Local).AddTicks(3801), new DateTime(2024, 4, 19, 13, 51, 11, 372, DateTimeKind.Local).AddTicks(3819) });

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 4, 19, 13, 51, 11, 430, DateTimeKind.Local).AddTicks(9355), new DateTime(2024, 4, 19, 13, 51, 11, 430, DateTimeKind.Local).AddTicks(9367) });
        }
    }
}
