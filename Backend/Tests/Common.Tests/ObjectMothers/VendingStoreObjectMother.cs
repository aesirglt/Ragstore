namespace Common.Tests.ObjectMothers;
using System;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;

public static partial class ObjectMother
{
    public static VendingStore VendingStore => new()
    {
        Id = 1,
        AccountId = 1,
        CharacterId = 1,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now,
        Name = "",
        ExpireDate = DateTime.Now,
        Map = "",
        Location = "",
        VendingStoreItems = [GetVendingStoreItem(1)]
    };
}
