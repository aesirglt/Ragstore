namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries;
using FunctionalConcepts.Results;

using MediatR;
using Totten.Solution.Ragstore.ApplicationService.ViewModels.Stores;

public class SearchedItemSumaryQuery : IRequest<Result<IQueryable<SearchedItemViewModel>>>;