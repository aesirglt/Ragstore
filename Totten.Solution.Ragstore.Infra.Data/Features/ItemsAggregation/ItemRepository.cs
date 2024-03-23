namespace Totten.Solution.Ragstore.Infra.Data.Features.ItemAggregation;
using Totten.Solution.Ragstore.Domain.Features.ItemsAggregation;
using Totten.Solution.Ragstore.Infra.Data.Bases;
using Totten.Solution.Ragstore.Infra.Data.Contexts.StoreContexts;

public class ItemRepository(RagnaStoreContext context)
    : RepositoryBase<Item>(context), IItemRepository
{
}
