namespace Totten.Solution.Ragstore.Domain.Features.HistoricAggregation.Bases;
using Totten.Solution.Ragstore.Domain.Bases;

public interface IHistoricRepository<THistoric> : IRepository<THistoric, int>
    where THistoric : HistoricBase<THistoric>
{

}