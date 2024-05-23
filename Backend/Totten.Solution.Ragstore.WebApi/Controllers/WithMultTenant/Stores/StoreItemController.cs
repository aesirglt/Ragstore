namespace Totten.Solution.Ragstore.WebApi.Controllers.WithMultTenant.Stores;

using Autofac;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.ResponseModels;
using Totten.Solution.Ragstore.WebApi.Bases;

[ApiController]
public class StoreItemController : BaseApiController
{
    const string API_ENDPOINT = "store-sumary";
    /// <summary>
    /// 
    /// </summary>
    /// <param name="lifetimeScope"></param>
    public StoreItemController(ILifetimeScope lifetimeScope) : base(lifetimeScope)
    {
    }

    [HttpGet($"vending-{API_ENDPOINT}/{{itemId}}")]
    public async Task<IActionResult> GetVending(
        [FromRoute] int itemId,
        [FromQuery] string server)
    {
        return await HandleQuery(new StoreItemValueSumaryQuery
        {
            ItemId = itemId,
            Server = server,
            StoreType =  StoreItemValueSumaryQuery.EStoreItemStoreType.Vending
        }, server);
    }

    [HttpGet($"buying-{API_ENDPOINT}/{{itemId}}")]
    public async Task<IActionResult> GetBuying(
        [FromRoute] int itemId,
        [FromQuery] string server)
    {
        return await HandleQuery(new StoreItemValueSumaryQuery
        {
            ItemId = itemId,
            Server = server,
            StoreType = StoreItemValueSumaryQuery.EStoreItemStoreType.Buying
        }, server);
    }
}
