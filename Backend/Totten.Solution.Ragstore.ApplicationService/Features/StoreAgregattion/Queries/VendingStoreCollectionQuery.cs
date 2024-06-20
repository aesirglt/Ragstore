namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries;

using FunctionalConcepts.Results;using FunctionalConcepts;
using MediatR;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;

public class VendingStoreCollectionQuery : IRequest<Result<IQueryable<VendingStore>>> { }
