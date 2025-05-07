namespace Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.Queries.Vendings;

using FunctionalConcepts.Results;
using MediatR;
using Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Vendings;

public class VendingStoreByIdQuery : IRequest<Result<VendingStore>>
{
    public int Id { get; set; }
}
