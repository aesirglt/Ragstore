namespace Totten.Solution.Ragstore.Infra.Data.Features.Characters;
using Totten.Solution.Ragstore.Domain.Features.Characters;
using Totten.Solution.Ragstore.Infra.Data.Bases;
using Totten.Solution.Ragstore.Infra.Data.Contexts.StoreContexts;

public class CharacterRepository(RagnaStoreContext context)
    : RepositoryBase<Character>(context), ICharacterRepository
{
}
