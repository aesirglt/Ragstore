namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries;

using MediatR;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.ResponseModels;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public class VendingStoreItemsCollectionQuery : IRequest<Result<Exception, IQueryable<StoreItemResponseModel>>>
{
    public string ItemName { get; set; } = "";
}
