namespace Totten.Solution.RagnaComercio.Infra.Data.Features.Users;
using Totten.Solution.RagnaComercio.Domain.Features.Users;
using Totten.Solution.RagnaComercio.Infra.Data.Bases;
using Totten.Solution.RagnaComercio.Infra.Data.Contexts.RagnaStoreContexts;

public class UserRepository(RagnaStoreContext context)
    : RepositoryBase<User, Guid>(context), IUserRepository
{
}
