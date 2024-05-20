using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Totten.Solution.Ragstore.Infra.Data.Migrations.RagnaStore
{
    /// <inheritdoc />
    public partial class CreateCallbackSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CallbacksSchedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sended = table.Column<bool>(type: "bit", nullable: false),
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SendIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallbacksSchedule", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Callbacks",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 5, 19, 20, 50, 2, 964, DateTimeKind.Local).AddTicks(9051), new DateTime(2024, 5, 19, 20, 50, 2, 964, DateTimeKind.Local).AddTicks(9055) });

            migrationBuilder.UpdateData(
                table: "Servers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 5, 19, 20, 50, 2, 964, DateTimeKind.Local).AddTicks(7125), new DateTime(2024, 5, 19, 20, 50, 2, 964, DateTimeKind.Local).AddTicks(7141) });

            migrationBuilder.UpdateData(
                table: "Servers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 5, 19, 20, 50, 2, 964, DateTimeKind.Local).AddTicks(7144), new DateTime(2024, 5, 19, 20, 50, 2, 964, DateTimeKind.Local).AddTicks(7145) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CallbacksSchedule");

            migrationBuilder.UpdateData(
                table: "Callbacks",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 5, 18, 18, 14, 6, 668, DateTimeKind.Local).AddTicks(8791), new DateTime(2024, 5, 18, 18, 14, 6, 668, DateTimeKind.Local).AddTicks(8794) });

            migrationBuilder.UpdateData(
                table: "Servers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 5, 18, 18, 14, 6, 668, DateTimeKind.Local).AddTicks(7567), new DateTime(2024, 5, 18, 18, 14, 6, 668, DateTimeKind.Local).AddTicks(7582) });

            migrationBuilder.UpdateData(
                table: "Servers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 5, 18, 18, 14, 6, 668, DateTimeKind.Local).AddTicks(7583), new DateTime(2024, 5, 18, 18, 14, 6, 668, DateTimeKind.Local).AddTicks(7584) });
        }
    }
}
