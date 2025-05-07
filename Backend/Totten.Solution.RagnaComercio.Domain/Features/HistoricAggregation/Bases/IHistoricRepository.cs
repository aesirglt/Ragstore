namespace Totten.Solution.RagnaComercio.Domain.Features.HistoricAggregation.Bases;
using Totten.Solution.RagnaComercio.Domain.Bases;

public interface IHistoricRepository<THistoric> : IRepository<THistoric, int>
    where THistoric : HistoricBase<THistoric>
{

}