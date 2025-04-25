namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries.Buyings;

using FunctionalConcepts.Results;
using MediatR;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Buyings;

public class BuyingStoreCollectionQuery : IRequest<Result<IQueryable<BuyingStore>>> { }
