namespace Totten.Solution.Ragstore.Domain.Features.StoresAgreggation.Vendings;
using Totten.Solution.Ragstore.Domain.Features.StoresAgreggation.Bases;

public class VendingStoreItem : StoreItem<VendingStoreItem>
{
    public long? ExpireDate { get; set; }
    public VendingStore VendingStore { get; set; }
}
