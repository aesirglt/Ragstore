namespace Totten.Solution.Ragstore.WebApi.Controllers.WithMultiTenant.Stores;

using Autofac;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.ResponseModels;
using Totten.Solution.Ragstore.WebApi.Bases;

[ApiController]
public class StoreItemController : BaseApiController
{
    const string API_ENDPOINT = "store-item";
    /// <summary>
    /// 
    /// </summary>
    /// <param name="lifetimeScope"></param>
    public StoreItemController(ILifetimeScope lifetimeScope) : base(lifetimeScope)
    {
    }

    [HttpGet($"{API_ENDPOINT}/{{itemId}}/sell")]
    public async Task<IActionResult> GetSell(
        [FromRoute] int itemId,
        [FromQuery] string server,
        ODataQueryOptions<StoreItemResponseModel> queryOptions)
    {
        return await HandleQueryable(new BuyingStoreItemsCollectionQuery
        {
            ItemName = itemName
        }, server, queryOptions);
    }

    [HttpGet($"{API_ENDPOINT}/{{itemId}}/buy")]
    public async Task<IActionResult> GetBuy(
        [FromRoute] int itemId,
        [FromQuery] string server,
        ODataQueryOptions<StoreItemResponseModel> queryOptions)
    {
        return await HandleQueryable(new BuyingStoreItemsCollectionQuery
        {
            ItemName = itemName
        }, server, queryOptions);
    }
}
