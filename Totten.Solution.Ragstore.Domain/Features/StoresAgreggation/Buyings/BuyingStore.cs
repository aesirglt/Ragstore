namespace Totten.Solution.Ragstore.Domain.Features.StoresAgreggation.Buyings;
using Totten.Solution.Ragstore.Domain.Features.StoresAgreggation.Bases;
public class BuyingStore : Store<BuyingStore>
{
    public BuyingStoreItem? BuyingStoreItem { get; set; }
    public int PriceLimit { get; set; }
}