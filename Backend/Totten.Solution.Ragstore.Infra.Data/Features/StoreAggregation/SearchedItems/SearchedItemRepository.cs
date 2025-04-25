namespace Totten.Solution.Ragstore.Infra.Data.Features.StoreAggregation.SearchedItems;
using Totten.Solution.Ragstore.Domain.Features.ItemsAggregation;
using Totten.Solution.Ragstore.Infra.Data.Bases;
using Totten.Solution.Ragstore.Infra.Data.Contexts.StoreServerContext;

public class SearchedItemRepository(ServerStoreContext context)
    : RepositoryBase<SearchedItem>(context), ISearchedItemRepository;