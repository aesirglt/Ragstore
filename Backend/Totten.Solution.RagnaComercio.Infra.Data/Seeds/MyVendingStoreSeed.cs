namespace Totten.Solution.RagnaComercio.Infra.Data.Seeds;

using Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Vendings;

public static class MyVendingStoreSeed
{
    public const int StoreId = 1;
    public const int StoreId2 = 2;
    public const string StoreName = "Store 4i20";
    public const string StoreName2 = "Lojinha 4i20";
    public const string Location = "150, 150";
    public const string Map = "prontera";

    public static List<VendingStore> Seed()
        => [
            new ()
            {
                Id = StoreId,
                Name = StoreName,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                AccountId = 1,
                CharacterId = 1,
                ExpireDate = DateTime.UtcNow,
                Location = Location,
                Map = Map
            },
            new ()
            {
                Id = StoreId2,
                Name = StoreName2,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                AccountId = 2,
                CharacterId = 2,
                ExpireDate = DateTime.UtcNow,
                Location = Location,
                Map = Map
            }
        ];
}
