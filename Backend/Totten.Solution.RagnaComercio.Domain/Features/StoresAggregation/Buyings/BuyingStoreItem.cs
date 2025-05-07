namespace Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Buyings;
using Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Bases;

public record  BuyingStoreItem : StoreItem<BuyingStoreItem>
{
    public virtual BuyingStore BuyingStore { get; set; }
}
