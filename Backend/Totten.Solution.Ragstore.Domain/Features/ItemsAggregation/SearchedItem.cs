namespace Totten.Solution.Ragstore.Domain.Features.ItemsAggregation;
using Totten.Solution.Ragstore.Domain.Bases;

public record SearchedItem : Entity<SearchedItem, int>
{
    public int ItemId { get; set; }
    public long Quantity { get; set; }
    public long Average { get; set; }
}
