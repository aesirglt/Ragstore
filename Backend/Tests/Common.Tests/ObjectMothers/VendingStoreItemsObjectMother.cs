namespace Common.Tests.ObjectMothers;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;

public static partial class ObjectMother
{
    public static VendingStoreItem GetVendingStoreItem(
        int itemId, string itemName, int itemPrice, int storeId,
        int accountId, int characterId, string character, string storeName, string map)
        => new()
        {
            Id = 0,
            ItemId = itemId,
            Name = itemName,
            Price = itemPrice,
            Quantity = 1,
            StoreId = storeId,
            AccountId = accountId,
            CharacterName = character,
            CharacterId = characterId,
            StoreName = storeName,
            UpdatedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            Map = map
        };
}
