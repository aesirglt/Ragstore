namespace Totten.Solution.Ragstore.Infra.Data.Features.Servers;
using Totten.Solution.Ragstore.Domain.Features.Servers;
using Totten.Solution.Ragstore.Infra.Data.Bases;
using Totten.Solution.Ragstore.Infra.Data.Contexts.StoreServerContext;

public class ServerRepository(ServerStoreContext context)
    : RepositoryBase<Server>(context), IServerRepository
{

}
