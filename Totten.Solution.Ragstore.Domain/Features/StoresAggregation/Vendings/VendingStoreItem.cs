namespace Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Bases;

public class VendingStoreItem : StoreItem<VendingStoreItem>
{
    public long? ExpireDate { get; set; }
    public int VendingStoreId { get; set; }
    //public virtual VendingStore VendingStore { get; set; }

    public VendingStoreItem()
    {
        //VendingStore = new VendingStore { Id = VendingStoreId };
    }
}
