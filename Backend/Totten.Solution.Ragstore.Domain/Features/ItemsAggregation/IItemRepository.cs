namespace Totten.Solution.Ragstore.Domain.Features.ItemsAggregation;

using Totten.Solution.Ragstore.Domain.Bases;

public interface IItemRepository : IRepository<Item, int>
{
    IQueryable<Item> GetAllByName(string name);
}
