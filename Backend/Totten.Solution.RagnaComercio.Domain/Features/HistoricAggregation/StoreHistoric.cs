namespace Totten.Solution.RagnaComercio.Domain.Features.HistoricAggregation;

using Totten.Solution.RagnaComercio.Domain.Features.HistoricAggregation.Bases;

public record StoreHistoric : HistoricBase<StoreHistoric>
{
    public string StoreType { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}
