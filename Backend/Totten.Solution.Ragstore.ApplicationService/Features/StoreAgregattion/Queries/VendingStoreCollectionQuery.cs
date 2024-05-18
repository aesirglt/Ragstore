namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries;

using MediatR;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public class VendingStoreCollectionQuery : IRequest<Result<Exception, IQueryable<VendingStore>>> { }
