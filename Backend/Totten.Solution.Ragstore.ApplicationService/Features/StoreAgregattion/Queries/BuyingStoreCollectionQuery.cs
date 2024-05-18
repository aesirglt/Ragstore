namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries;

using MediatR;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Buyings;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public class BuyingStoreCollectionQuery : IRequest<Result<Exception, IQueryable<BuyingStore>>> { }
