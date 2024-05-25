namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries;

using LanguageExt.Common;
using MediatR;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.ResponseModels;

public class StoreItemValueSumaryQuery : IRequest<Result<StoreItemValueSumaryResponseModel>>
{
    public required int ItemId { get; init; }
    public required string Server { get; init; }
    public required EStoreItemStoreType StoreType { get; init; }

    public enum EStoreItemStoreType
    {
        Vending,
        Buying
    }
}
