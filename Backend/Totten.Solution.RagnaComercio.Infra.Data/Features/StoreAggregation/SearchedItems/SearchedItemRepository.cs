namespace Totten.Solution.RagnaComercio.Infra.Data.Features.StoreAggregation.SearchedItems;
using Totten.Solution.RagnaComercio.Domain.Features.ItemsAggregation;
using Totten.Solution.RagnaComercio.Infra.Data.Bases;
using Totten.Solution.RagnaComercio.Infra.Data.Contexts.StoreServerContext;

public class SearchedItemRepository(ServerStoreContext context)
    : RepositoryBase<SearchedItem, int>(context), ISearchedItemRepository;