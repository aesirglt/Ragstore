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
    {
        IQueryable<SearchedItemViewModel> result =
            _repository.GetAll()
            .GroupBy(x => x.ItemId)
            .Select(g => g.OrderByDescending(x => x.CreatedAt).Select(s => new SearchedItemViewModel
            {
                ItemId = s.ItemId,
                ItemName = s.Name,
                Average = s.Average,
                Quantity = s.Quantity,
                Image = "https://static.divine-pride.net/images/items/item/" + s.ItemId + ".png"
            }).First())
            .Take(10);

        return await Result.Of(result).AsTask();
    }
}
