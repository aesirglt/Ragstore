namespace Totten.Solution.RagnaComercio.Infra.Data.Features.Characters;
using Totten.Solution.RagnaComercio.Domain.Features.Characters;
using Totten.Solution.RagnaComercio.Infra.Data.Bases;
using Totten.Solution.RagnaComercio.Infra.Data.Contexts.StoreServerContext;

public class CharacterRepository(ServerStoreContext context)
    : RepositoryBase<Character, int>(context), ICharacterRepository
{
}
