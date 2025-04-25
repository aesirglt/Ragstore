namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries.Vendings;

using FunctionalConcepts.Results;
using MediatR;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.ResponseModels;

public class VendingStoreItemsCollectionQuery : IRequest<Result<IQueryable<StoreItemResponseModel>>>;