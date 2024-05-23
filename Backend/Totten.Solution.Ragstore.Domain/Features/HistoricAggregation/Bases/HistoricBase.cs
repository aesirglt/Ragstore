namespace Totten.Solution.Ragstore.Domain.Features.HistoricAggregation.Bases;
using Totten.Solution.Ragstore.Domain.Bases;

public record HistoricBase<THistoric> : Entity<THistoric, int>
    where THistoric : HistoricBase<THistoric>
{
    public string Server { get; set; } = string.Empty;
}
