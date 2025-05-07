using Totten.Solution.RagnaComercio.Domain.Features.HistoricAggregation.Bases;

namespace Totten.Solution.RagnaComercio.Domain.Features.HistoricAggregation;
public record ItemSearchedHistoric : HistoricBase<ItemSearchedHistoric>
{
    public int Price { get; set; }
    public long Quantity { get; set; }
}
