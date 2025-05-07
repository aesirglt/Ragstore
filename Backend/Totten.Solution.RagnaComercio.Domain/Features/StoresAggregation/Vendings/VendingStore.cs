namespace Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Vendings;

using Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Bases;

public record VendingStore : Store<VendingStore>
{
    public virtual List<VendingStoreItem> VendingStoreItems { get; set; } = [];

    public VendingStore()
    {
    }
}