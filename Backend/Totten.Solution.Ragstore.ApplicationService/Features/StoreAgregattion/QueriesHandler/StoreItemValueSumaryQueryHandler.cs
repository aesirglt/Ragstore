namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.QueriesHandler;

using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.ResponseModels;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;
using static Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries.StoreItemValueSumaryQuery;

public class StoreItemValueSumaryQueryHandler : IRequestHandler<StoreItemValueSumaryQuery, Result<Exception, StoreItemValueSumaryResponseModel>>
{
    public Task<Result<Exception, StoreItemValueSumaryResponseModel>> Handle(StoreItemValueSumaryQuery request, CancellationToken cancellationToken)
    {
        return request.StoreType switch
        {
            EStoreItemStoreType.Vending => GetFromVending(),
            EStoreItemStoreType.Buying => GetFromBuying(),
            _ => throw new NotImplementedException()
        };
    }

    private async Task<Result<Exception, StoreItemValueSumaryResponseModel>> GetFromBuying()
    {
        throw new NotImplementedException();
    }

    private async Task<Result<Exception, StoreItemValueSumaryResponseModel>> GetFromVending()
    {

        throw new NotImplementedException();
    }

}
