namespace Totten.Solution.RagnaComercio.Domain.Features.HistoricAggregation.Bases;
using Totten.Solution.RagnaComercio.Domain.Bases;

public record HistoricBase<THistoric> : Entity<THistoric, int>
    where THistoric : HistoricBase<THistoric>
{
    public string Server { get; set; } = string.Empty;
}
