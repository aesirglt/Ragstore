namespace Totten.Solution.Ragstore.Domain.Features.StoresAgreggation.Vendings;

using Totten.Solution.Ragstore.Domain.Features.StoresAgreggation.Bases;

public class VendingStore : Store<VendingStore>
{
    public List<VendingStoreItem> VendingStoreItems { get; set; } = new();
}