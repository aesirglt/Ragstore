namespace Common.Tests.ObjectMothers;

using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Commands;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Commons;

public static partial class ObjectMother
{
    public static VendingStoreItemCommand GetItemStoreCmd(
        int itemId,
        string name,
        double price,
        int quantity)
        => new()
        {
            ItemId = itemId,
            Name = name,
            Price = price,
            Quantity = quantity,
        };

    public static BuyingStoreItemCommand GetBuyItemStoreCmd(
        int itemId,
        string name,
        double price,
        int quantity)
        => new()
        {
            ItemId = itemId,
            Name = name,
            Price = price,
            Quantity = quantity,
        };

    public static VendingStoreSaveCommand GetVendingStoreSaveCmd() => new()
    {
        Map = "Prontera",
        AccountId = 1,
        CharacterId = 1,
        CharacterName = "",
        ExpireDate = DateTime.UtcNow,
        Name = "Lojinha 4i20",
        Server = "bRO - thor",
        Location = "150,150",
        StoreItems = new()
        {
            GetItemStoreCmd(ItemRedPotion.Id, ItemRedPotion.Name, 100, 1000)
        }
    };

    public static BuyingStoreSaveCommand GetBuyingStoreSaveCmd() => new()
    {
        Map = "Prontera",
        AccountId = 1,
        CharacterId = 1,
        CharacterName = "",
        ExpireDate = DateTime.UtcNow,
        Name = "Lojinha 4i20",
        Server = "bRO - thor",
        Location = "150,150",
        StoreItems = new()
        {
            GetBuyItemStoreCmd(ItemRedPotion.Id, ItemRedPotion.Name, 100, 1000)
        }
    };
}