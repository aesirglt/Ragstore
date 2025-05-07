namespace Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.Queries;
using FunctionalConcepts.Results;

using MediatR;
using Totten.Solution.RagnaComercio.ApplicationService.ViewModels.Stores;

public class SearchedItemSumaryQuery : IRequest<Result<IQueryable<SearchedItemViewModel>>>;