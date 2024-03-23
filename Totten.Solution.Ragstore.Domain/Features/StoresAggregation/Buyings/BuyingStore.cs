namespace Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Buyings;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Bases;
public class BuyingStore : Store<BuyingStore>
{
    public virtual BuyingStoreItem? BuyingStoreItem { get; set; }
    public int PriceLimit { get; set; }
}