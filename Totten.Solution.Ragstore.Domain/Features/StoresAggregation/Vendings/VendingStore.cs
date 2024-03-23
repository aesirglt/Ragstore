namespace Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;

using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Bases;

public class VendingStore : Store<VendingStore>
{
    public List<VendingStoreItem> VendingStoreItems { get; set; } = new();
}