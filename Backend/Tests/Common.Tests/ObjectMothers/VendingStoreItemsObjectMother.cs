namespace Common.Tests.ObjectMothers;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;

public static partial class ObjectMother
{
    public static VendingStoreItem GetVendingStoreItem(int storeId)
        => new()
        {
            Id = 1,
            ItemId = 2,
            Name = "",
            Price = 3,
            Quantity = 4,
            StoreId = storeId,
            AccountId = 5,
            CharacterName = "",
            CharacterId = 6,
            StoreName = "",
            UpdatedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            Map = ""
        };
}
