namespace Totten.Solution.RagnaComercio.Domain.Features.ItemsAggregation;
using Totten.Solution.RagnaComercio.Domain.Bases;
using Totten.Solution.RagnaComercio.Domain.Features.Servers;

public record SearchedItem : Entity<SearchedItem, int>
{
    public int ItemId { get; set; }
    public long Quantity { get; set; }
    public long Average { get; set; }
}
