namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries;

using LanguageExt.Common;
using MediatR;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Buyings;

public class BuyingStoreCollectionQuery : IRequest<Result<IQueryable<BuyingStore>>> { }
