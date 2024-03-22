namespace Totten.Solution.Ragstore.Infra.Data.Features.Callbacks;

using System.Collections.Generic;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.Domain.Features.Callbacks;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;
using Totten.Solution.Ragstore.Infra.Data.Contexts.StoreContexts;

public class CallbackRepository : ICallbackRepository
{
    private readonly RagnaStoreContext _context;

    public CallbackRepository(RagnaStoreContext context)
    {
        _context = context;
    }

    public Task<List<Callback>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<List<Callback>> GetAllByUser(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Callback>> GetForCallback(string server, string itemName, double value)
    {
        throw new NotImplementedException();
    }

    public Task<Unit> Save(Callback item)
    {
        throw new NotImplementedException();
    }
}
