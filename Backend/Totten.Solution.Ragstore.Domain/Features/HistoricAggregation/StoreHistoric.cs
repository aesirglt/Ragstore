namespace Totten.Solution.Ragstore.Domain.Features.HistoricAggregation;
public record StoreHistoric : HistoricBase
{
    public string StoreType { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}
