namespace Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.QueriesHandler;
using FunctionalConcepts.Results;
using MediatR;
using Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.Queries;
using Totten.Solution.RagnaComercio.ApplicationService.ViewModels.Stores;
using Totten.Solution.RagnaComercio.Domain.Features.ItemsAggregation;
using Totten.Solution.RagnaComercio.Infra.Cross.Statics;

public class SearchedItemSumaryQueryHandler(
    ISearchedItemRepository repository)
    : IRequestHandler<SearchedItemSumaryQuery, Result<IQueryable<SearchedItemViewModel>>>
{
    private readonly ISearchedItemRepository _repository = repository;

    public async Task<Result<IQueryable<SearchedItemViewModel>>> Handle(
        SearchedItemSumaryQuery request,
        CancellationToken cancellationToken)
        => await Result.Of(_repository.GetAll()
                .GroupBy(x => x.ItemId)
                .Select(g => g.OrderByDescending(x => x.CreatedAt).First())
                .Select(x => new SearchedItemViewModel
                {
                    ItemId = x.ItemId,
                    ItemName = x.Name,
                    Average = x.Average,
                    Quantity = x.Quantity,
                    Image = "https://static.divine-pride.net/images/items/item/" + x.ItemId + ".png"
                })).AsTask();
}
