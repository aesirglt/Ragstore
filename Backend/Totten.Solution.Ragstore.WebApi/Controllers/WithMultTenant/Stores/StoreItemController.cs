namespace Totten.Solution.Ragstore.WebApi.Controllers.WithMultTenant.Stores;

using Autofac;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries.Vendings;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.ResponseModels;
using Totten.Solution.Ragstore.ApplicationService.ViewModels.Stores;
using Totten.Solution.Ragstore.WebApi.Bases;

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
    /// <param name="queryOptions"></param>
    /// <returns></returns>
    [HttpGet($"{{server}}/{API_ENDPOINT}")]
    [ProducesResponseType<StoreItemResponseModel>(statusCode: 200)]
    public async Task<IActionResult> GetAllItems(
        [FromRoute] string server,
        [FromQuery] string? storeType,
        ODataQueryOptions<StoreItemResponseModel> queryOptions)
    {
        return await HandleQueryable(new StoreItemsCollectionQuery
        {
            StoreType = storeType == "buying" ? StoreItemValueSumaryQuery.EStoreItemStoreType.Buying : StoreItemValueSumaryQuery.EStoreItemStoreType.Vending
        }, server, queryOptions);
    }
}
