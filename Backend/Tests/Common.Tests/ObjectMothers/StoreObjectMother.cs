namespace Common.Tests.ObjectMothers;
using System;
using Totten.Solution.RagnaComercio.Domain.Features.Characters;
using Totten.Solution.RagnaComercio.Domain.Features.ItemsAggregation;
using Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Buyings;
using Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Vendings;

public static partial class ObjectMother
{
    private static int _vendingId = 0;
    private static int _vendingAccountId = 1;

    private static string _vendingName = "lojinha";

    private static string _vendingMap = "Prontera";
    private static string _vendingLocation = "150,150";

    public static BuyingStore GetBuyingStore(Character character, Item item) => new()
    {
        Id = _vendingId,
        AccountId = _vendingAccountId,
        CharacterId = character.Id,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow,
        Name = _vendingName,
        ExpireDate = null,
        Map = _vendingMap,
        Location = _vendingLocation,
        BuyingStoreItem = new BuyingStoreItem
        {
            Id = 0,
            Name = item.Name,
            ItemId = item.Id,
            CharacterId = character.Id,
            CharacterName = character.Name,
            CreatedAt = DateTime.UtcNow,
            Price = 0,
            Quantity = 100,
            Map = $"{_vendingMap} {_vendingLocation}",
            UpdatedAt = DateTime.UtcNow,
            AccountId = _vendingAccountId,
            StoreName = _vendingName,
        }
    };

    public static VendingStore GetVendingStore(Character character, List<Item> items) => new()
    {
        Id = _vendingId,
        AccountId = _vendingAccountId,
        CharacterId = character.Id,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow,
        Name = _vendingName,
        ExpireDate = null,
        Map = _vendingMap,
        Location = _vendingLocation,
        VendingStoreItems =
            items.Select((item, it)
                    => GetVendingStoreItem(item.Id, item.Name,
                        itemPrice: 100, _vendingId, _vendingAccountId,
                        character.Id, character.Name, _vendingName,
                        $"{_vendingMap} {_vendingLocation}"))
            .ToList()
    };
}
