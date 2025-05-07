namespace Totten.Solution.RagnaComercio.Infra.Data.Features.Accounts;
using Totten.Solution.RagnaComercio.Domain.Features.Accounts;
using Totten.Solution.RagnaComercio.Infra.Data.Bases;
using Totten.Solution.RagnaComercio.Infra.Data.Contexts.RagnaStoreContexts;

public class AccountRepository(RagnaStoreContext context)
    : RepositoryBase<Account, int>(context), IAccountRepository
{
}
