namespace Totten.Solution.RagnaComercio.Infra.Data.Features.CallbackAggregation;
using Totten.Solution.RagnaComercio.Domain.Features.CallbackAggregation;
using Totten.Solution.RagnaComercio.Infra.Data.Bases;
using Totten.Solution.RagnaComercio.Infra.Data.Contexts.RagnaStoreContexts;

public class CallbackRepository(RagnaStoreContext context)
    : RepositoryBase<Callback, Guid>(context), ICallbackRepository
{
}
