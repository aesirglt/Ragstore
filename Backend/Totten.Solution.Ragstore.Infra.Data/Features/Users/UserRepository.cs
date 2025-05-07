namespace Totten.Solution.Ragstore.Infra.Data.Features.Users;
using Totten.Solution.Ragstore.Domain.Features.Users;
using Totten.Solution.Ragstore.Infra.Data.Bases;
using Totten.Solution.Ragstore.Infra.Data.Contexts.RagnaStoreContexts;

public class UserRepository(RagnaStoreContext context)
    : RepositoryBase<User, Guid>(context), IUserRepository
{
}
