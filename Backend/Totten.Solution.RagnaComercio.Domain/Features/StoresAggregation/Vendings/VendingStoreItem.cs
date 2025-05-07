namespace Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Vendings;
using Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Bases;

public record VendingStoreItem : StoreItem<VendingStoreItem>
{
    public long? ExpireDate { get; set; }

    public VendingStoreItem() { }
}
