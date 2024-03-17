namespace Totten.Solution.Ragstore.Domain.Features.Callbacks;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public interface ICallbackRepository
{
    Task<List<Callback>> GetForCallback(string server, string itemName, double value);
    Task<List<Callback>> GetAllByUser(string userId);
    Task<List<Callback>> GetAll();
    Task<Unit> Save(Callback item);
}
