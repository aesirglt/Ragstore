namespace Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.Queries;

using FunctionalConcepts.Results;
using MediatR;
using Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.ResponseModels;

public class StoreItemValueSumaryQuery : IRequest<Result<StoreItemValueSumaryResponseModel>>
{
    public required int ItemId { get; init; }
    public required EStoreItemStoreType StoreType { get; init; }

    public enum EStoreItemStoreType
    {
        Vending,
        Buying
    }
}
