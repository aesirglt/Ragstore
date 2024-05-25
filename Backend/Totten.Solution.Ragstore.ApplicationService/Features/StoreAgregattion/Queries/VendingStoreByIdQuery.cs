namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries;

using LanguageExt.Common;
using MediatR;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;

public class VendingStoreByIdQuery : IRequest<Result<VendingStore>>
{
    public int Id { get; set; }
}
