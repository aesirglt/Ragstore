namespace Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Buyings;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Bases;

public record BuyingStoreItem : StoreItem<BuyingStoreItem>
{
    public virtual BuyingStore BuyingStore { get; set; }
}
