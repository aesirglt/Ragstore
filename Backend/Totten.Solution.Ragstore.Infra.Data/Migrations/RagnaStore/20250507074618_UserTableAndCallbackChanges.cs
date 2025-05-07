using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Totten.Solution.Ragstore.Infra.Data.Migrations.RagnaStore
{
    /// <inheritdoc />
    public partial class UserTableAndCallbackChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "Callbacks");

            migrationBuilder.DropColumn(
                name: "UserCellphone",
                table: "Callbacks");

            migrationBuilder.Sql(@"
                ALTER TABLE ""Callbacks""
                ALTER COLUMN ""CallbackOwnerId"" TYPE uuid
                USING ""CallbackOwnerId""::uuid;
            ");

            migrationBuilder.AlterColumn<Guid>(
            name: "CallbackOwnerId",
            table: "Callbacks",
            type: "uuid",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: false);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Callbacks",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    NormalizedEmail = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    AvatarUrl = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SearchCount = table.Column<long>(type: "bigint", nullable: false),
                    ReceivePriceAlerts = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Callbacks",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CallbackOwnerId", "CreatedAt", "UpdatedAt", "UserId" },
                values: new object[] { new Guid("d7aeb595-44a5-4f5d-822e-980f35ace12d"), new DateTime(2025, 5, 7, 7, 46, 18, 114, DateTimeKind.Utc).AddTicks(1528), new DateTime(2025, 5, 7, 7, 46, 18, 114, DateTimeKind.Utc).AddTicks(1532), null });

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
                name: "IX_Callbacks_UserId",
                table: "Callbacks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Callbacks_Users_UserId",
                table: "Callbacks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Callbacks_Users_UserId",
                table: "Callbacks");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Callbacks_UserId",
                table: "Callbacks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Callbacks");

            migrationBuilder.AlterColumn<string>(
                name: "CallbackOwnerId",
                table: "Callbacks",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Callbacks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserCellphone",
                table: "Callbacks",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Callbacks",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CallbackOwnerId", "CreatedAt", "Level", "UpdatedAt", "UserCellphone" },
                values: new object[] { "d7aeb595-44a5-4f5d-822e-980f35ace12d", new DateTime(2025, 4, 12, 19, 31, 3, 792, DateTimeKind.Utc).AddTicks(9411), 4, new DateTime(2025, 4, 12, 19, 31, 3, 792, DateTimeKind.Utc).AddTicks(9412), "+5584988633251" });

            migrationBuilder.UpdateData(
                table: "Servers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 4, 12, 19, 31, 3, 792, DateTimeKind.Utc).AddTicks(6713), new DateTime(2025, 4, 12, 19, 31, 3, 792, DateTimeKind.Utc).AddTicks(6715) });

            migrationBuilder.UpdateData(
                table: "Servers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 4, 12, 19, 31, 3, 792, DateTimeKind.Utc).AddTicks(6718), new DateTime(2025, 4, 12, 19, 31, 3, 792, DateTimeKind.Utc).AddTicks(6718) });
        }
    }
}
