namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries;

using MediatR;
using System;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.ResponseModels;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public class StoreItemValueSumaryQuery : IRequest<Result<Exception, StoreItemValueSumaryResponseModel>>
{
    public required EStoreItemStoreType StoreType { get; init; }

    public enum EStoreItemStoreType
    {
        Vending,
        Buying
    }
}
