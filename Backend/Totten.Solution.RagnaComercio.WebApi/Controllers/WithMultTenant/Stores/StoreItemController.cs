namespace Totten.Solution.RagnaComercio.WebApi.Controllers.WithMultTenant.Stores;

using Autofac;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.Queries;
using Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.QueriesHandler;
using Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.ResponseModels;
using Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Vendings;
using Totten.Solution.RagnaComercio.WebApi.Bases;

/// <summary>
/// 
/// </summary>
/// <remarks>
/// 
/// </remarks>
/// <param name="lifetimeScope"></param>
[ApiController]
public class StoreItemController(ILifetimeScope lifetimeScope) : BaseApiController(lifetimeScope)
{
    const string API_ENDPOINT = "store-items";

    /// <summary>
    /// 
    /// </summary>
    /// <param name="server"></param>
    /// <param name="storeType"></param>
    /// <param name="itemId"></param>
    /// <param name="queryOptions"></param>
    /// <returns></returns>
    [HttpGet($"{{server}}/{API_ENDPOINT}")]
    [ProducesResponseType<StoreItemResumeViewModel>(statusCode: 200)]
    public async Task<IActionResult> GetAllItems(
        [FromRoute] string server,
        [FromQuery] string? storeType,
        ODataQueryOptions<StoreItemResumeViewModel> queryOptions)
    {
        return await HandleQueryable(new StoreItemResumeQuery
        {
            StoreType = storeType ?? nameof(VendingStore),
        }, server, queryOptions);
    }
}
