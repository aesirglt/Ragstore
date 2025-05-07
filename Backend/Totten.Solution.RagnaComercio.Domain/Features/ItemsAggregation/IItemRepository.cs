namespace Totten.Solution.RagnaComercio.Domain.Features.ItemsAggregation;

using Totten.Solution.RagnaComercio.Domain.Bases;

public interface IItemRepository : IRepository<Item, int>
{
    IQueryable<Item> GetAllByName(string name);
}
