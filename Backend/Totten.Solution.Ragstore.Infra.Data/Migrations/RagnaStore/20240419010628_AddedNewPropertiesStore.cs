using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Totten.Solution.Ragstore.Infra.Data.Migrations.RagnaStore
{
    /// <inheritdoc />
    public partial class AddedNewPropertiesStore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BuyingStoreItems_BuyingStores_BuyingStoreId",
                table: "BuyingStoreItems");

            migrationBuilder.DropForeignKey(
                name: "FK_VendingStoreItems_VendingStores_VendingStoreId",
                table: "VendingStoreItems");

            migrationBuilder.RenameColumn(
                name: "VendingStoreId",
                table: "VendingStoreItems",
                newName: "StoreId");

            migrationBuilder.RenameIndex(
                name: "IX_VendingStoreItems_VendingStoreId",
                table: "VendingStoreItems",
                newName: "IX_VendingStoreItems_StoreId");

            migrationBuilder.RenameColumn(
                name: "BuyingStoreId",
                table: "BuyingStoreItems",
                newName: "StoreId");

            migrationBuilder.RenameIndex(
                name: "IX_BuyingStoreItems_BuyingStoreId",
                table: "BuyingStoreItems",
                newName: "IX_BuyingStoreItems_StoreId");

            migrationBuilder.AddColumn<string>(
                name: "CharacterName",
                table: "VendingStoreItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Map",
                table: "VendingStoreItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StoreName",
                table: "VendingStoreItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ChatId",
                table: "EquipmentItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CharacterName",
                table: "BuyingStoreItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Map",
                table: "BuyingStoreItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StoreName",
                table: "BuyingStoreItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.AddForeignKey(
                name: "FK_BuyingStoreItems_BuyingStores_StoreId",
                table: "BuyingStoreItems",
                column: "StoreId",
                principalTable: "BuyingStores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VendingStoreItems_VendingStores_StoreId",
                table: "VendingStoreItems",
                column: "StoreId",
                principalTable: "VendingStores",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BuyingStoreItems_BuyingStores_StoreId",
                table: "BuyingStoreItems");

            migrationBuilder.DropForeignKey(
                name: "FK_VendingStoreItems_VendingStores_StoreId",
                table: "VendingStoreItems");

            migrationBuilder.DropColumn(
                name: "CharacterName",
                table: "VendingStoreItems");

            migrationBuilder.DropColumn(
                name: "Map",
                table: "VendingStoreItems");

            migrationBuilder.DropColumn(
                name: "StoreName",
                table: "VendingStoreItems");

            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "EquipmentItems");

            migrationBuilder.DropColumn(
                name: "CharacterName",
                table: "BuyingStoreItems");

            migrationBuilder.DropColumn(
                name: "Map",
                table: "BuyingStoreItems");

            migrationBuilder.DropColumn(
                name: "StoreName",
                table: "BuyingStoreItems");

            migrationBuilder.RenameColumn(
                name: "StoreId",
                table: "VendingStoreItems",
                newName: "VendingStoreId");

            migrationBuilder.RenameIndex(
                name: "IX_VendingStoreItems_StoreId",
                table: "VendingStoreItems",
                newName: "IX_VendingStoreItems_VendingStoreId");

            migrationBuilder.RenameColumn(
                name: "StoreId",
                table: "BuyingStoreItems",
                newName: "BuyingStoreId");

            migrationBuilder.RenameIndex(
                name: "IX_BuyingStoreItems_StoreId",
                table: "BuyingStoreItems",
                newName: "IX_BuyingStoreItems_BuyingStoreId");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 3, 23, 12, 10, 23, 205, DateTimeKind.Local).AddTicks(140), new DateTime(2024, 3, 23, 12, 10, 23, 205, DateTimeKind.Local).AddTicks(156) });

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 3, 23, 12, 10, 23, 205, DateTimeKind.Local).AddTicks(9784), new DateTime(2024, 3, 23, 12, 10, 23, 205, DateTimeKind.Local).AddTicks(9791) });

            migrationBuilder.AddForeignKey(
                name: "FK_BuyingStoreItems_BuyingStores_BuyingStoreId",
                table: "BuyingStoreItems",
                column: "BuyingStoreId",
                principalTable: "BuyingStores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VendingStoreItems_VendingStores_VendingStoreId",
                table: "VendingStoreItems",
                column: "VendingStoreId",
                principalTable: "VendingStores",
                principalColumn: "Id");
        }
    }
}
