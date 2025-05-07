namespace Totten.Solution.RagnaComercio.Infra.Data.Seeds;

using Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Vendings;

public static class MyVendingItemsSeed
{
    public static List<VendingStoreItem> Seed()
        => [
            new ()
            {
                Id = 1,
                ItemId = 501,
                Name = "Poção Vermelha",
                Price = 200,
                Map = MyVendingStoreSeed.Map + " " + MyVendingStoreSeed.Location,
                StoreId = MyVendingStoreSeed.StoreId,
                StoreName = MyVendingStoreSeed.StoreName,
                Location = 0,
                AccountId= 1,
                CharacterId= 1,
                IsIdentified = 0,
                CharacterName = MyCharacterSeed.CharName,
                EnchantGrade = 0,
                CreatedAt= DateTime.UtcNow,
                UpdatedAt= DateTime.UtcNow,
                Quantity = 100,
            },
            new ()
            {
                Id = 2,
                ItemId = 503,
                Name = "Poção Amarela",
                Price = 250,
                Map = MyVendingStoreSeed.Map + " " + MyVendingStoreSeed.Location,
                StoreId = MyVendingStoreSeed.StoreId,
                StoreName = MyVendingStoreSeed.StoreName,
                Location = 0,
                AccountId = 1,
                CharacterId = 1,
                IsIdentified = 0,
                CharacterName = MyCharacterSeed.CharName,
                EnchantGrade = 0,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Quantity = 1000,
            },
            new ()
            {
                Id = 3,
                ItemId = 501,
                Name = "Poção Vermelha",
                Price = 500,
                Map = MyVendingStoreSeed.Map + " " + MyVendingStoreSeed.Location,
                StoreId = MyVendingStoreSeed.StoreId2,
                StoreName = MyVendingStoreSeed.StoreName2,
                Location = 0,
                AccountId = 2,
                CharacterId = 2,
                IsIdentified = 0,
                CharacterName = MyCharacterSeed.CharName2,
                EnchantGrade = 0,
                CreatedAt= DateTime.UtcNow,
                UpdatedAt= DateTime.UtcNow,
                Quantity = 200,
            }
        ];
}
