namespace Totten.Solution.Ragstore.Domain.Features.StoresAgreggation.Buyings;
using Totten.Solution.Ragstore.Domain.Features.StoresAgreggation.Bases;

public class BuyingStoreItem : StoreItem<BuyingStoreItem>
{
    public int BuyingStoreId { get; set; }
    public BuyingStore BuyingStore { get; set; }
}
