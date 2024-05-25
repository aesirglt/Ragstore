namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries;

using LanguageExt.Common;
using MediatR;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;

public class VendingStoreCollectionQuery : IRequest<Result<IQueryable<VendingStore>>> { }
