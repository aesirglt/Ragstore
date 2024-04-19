namespace Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Bases;

public record VendingStoreItem : StoreItem<VendingStoreItem>
{
    public long? ExpireDate { get; set; }

    public VendingStoreItem() { }
}
