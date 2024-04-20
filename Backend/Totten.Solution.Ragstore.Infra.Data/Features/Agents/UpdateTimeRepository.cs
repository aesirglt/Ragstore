namespace Totten.Solution.Ragstore.Infra.Data.Features.Agents;
using Totten.Solution.Ragstore.Domain.Features.Agents;
using Totten.Solution.Ragstore.Infra.Data.Bases;
using Totten.Solution.Ragstore.Infra.Data.Contexts.StoreContexts;

public class UpdateTimeRepository(RagnaStoreContext context)
    : RepositoryBase<UpdateTime>(context), IUpdateTimeRepository
{
}
