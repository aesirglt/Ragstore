namespace Totten.Solution.RagnaComercio.Infra.Data.Features.Servers;
using Totten.Solution.RagnaComercio.Domain.Features.Servers;
using Totten.Solution.RagnaComercio.Infra.Data.Bases;
using Totten.Solution.RagnaComercio.Infra.Data.Contexts.RagnaStoreContexts;

public sealed class ServerRepository(RagnaStoreContext context)
    : RepositoryBase<Server, Guid>(context), IServerRepository
{

}
