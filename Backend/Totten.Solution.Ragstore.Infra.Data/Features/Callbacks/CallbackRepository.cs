namespace Totten.Solution.Ragstore.Infra.Data.Features.Callbacks;
using Totten.Solution.Ragstore.Domain.Features.Callbacks;
using Totten.Solution.Ragstore.Infra.Data.Bases;
using Totten.Solution.Ragstore.Infra.Data.Contexts.StoreContexts;

public class CallbackRepository(RagnaStoreContext context)
    : RepositoryBase<Callback>(context), ICallbackRepository
{

}
