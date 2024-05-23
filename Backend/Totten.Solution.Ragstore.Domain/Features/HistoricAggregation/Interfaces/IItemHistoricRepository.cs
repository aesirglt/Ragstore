namespace Totten.Solution.Ragstore.Domain.Features.HistoricAggregation.Interfaces;
using Totten.Solution.Ragstore.Domain.Features.HistoricAggregation.Bases;

public interface IItemHistoricRepository : IHistoricRepository<ItemSearchedHistoric>
{
    ItemSearchedHistoric? GetByItemId(int itemId);
}
