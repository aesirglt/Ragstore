namespace Totten.Solution.Ragstore.Domain.Features.StoresAgreggation.Vendings;
using Totten.Solution.Ragstore.Domain.Features.StoresAgreggation.Bases;

public class VendingStoreItem : StoreItem
{
    public VendingStore VendingStore { get; set; }
    public long? ExpireDate { get; set; }
}
