namespace Totten.Solution.Ragstore.Infra.Data.Features.Accounts;
using Totten.Solution.Ragstore.Domain.Features.Accounts;
using Totten.Solution.Ragstore.Infra.Data.Bases;
using Totten.Solution.Ragstore.Infra.Data.Contexts.RagnaStoreContexts;

public class AccountRepository(RagnaStoreContext context)
    : RepositoryBase<Account, int>(context), IAccountRepository
{
}
