namespace Totten.Solution.Ragstore.Domain.Features.Items;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public interface IItemRepository
{
    Task<List<Item>> GetAll();
    Task<Unit> Save(Item item);
}
