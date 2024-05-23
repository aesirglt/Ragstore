using Totten.Solution.Ragstore.Domain.Features.HistoricAggregation.Bases;

namespace Totten.Solution.Ragstore.Domain.Features.HistoricAggregation;
public record ItemSearchedHistoric : HistoricBase<ItemSearchedHistoric>
{
    public int Price { get; set; }
    public long Quantity { get; set; }
}
