namespace Totten.Solution.RagnaComercio.Domain.Features.HistoricAggregation.Interfaces;
using Totten.Solution.RagnaComercio.Domain.Features.HistoricAggregation.Bases;

public interface IItemHistoricRepository : IHistoricRepository<ItemSearchedHistoric>
{
    ItemSearchedHistoric? GetByItemId(int itemId);
}
