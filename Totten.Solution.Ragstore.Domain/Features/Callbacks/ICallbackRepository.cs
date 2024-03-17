namespace Totten.Solution.Ragstore.Domain.Features.Callbacks;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public interface ICallbackRepository
{
    Task<List<Callback>> GetByItemAll(string itemName);
    Task<List<Callback>> GetAll();
    Task<Unit> Save(Callback item);
}
