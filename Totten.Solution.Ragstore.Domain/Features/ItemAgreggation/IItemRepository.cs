namespace Totten.Solution.Ragstore.Domain.Features.ItemAgreggation;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public interface IItemRepository
{
    Task<List<Item>> GetAll();
    Task<List<Item>> GetAllByDate(string name, DateTime date);
    Task<Unit> Save(Item item);
}
