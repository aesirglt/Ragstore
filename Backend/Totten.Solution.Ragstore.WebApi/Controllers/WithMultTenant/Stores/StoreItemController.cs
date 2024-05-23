namespace Totten.Solution.Ragstore.WebApi.Controllers.WithMultTenant.Stores;

using Autofac;
using Microsoft.AspNetCore.Mvc;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries;
using Totten.Solution.Ragstore.WebApi.Bases;

/// <summary>
/// 
/// </summary>
[ApiController]
public class StoreItemController : BaseApiController
{
    const string API_ENDPOINT = "store-sumary";
    /// <summary>
    /// 
    /// </summary>
    /// <param name="lifetimeScope"></param>
    public StoreItemController(ILifetimeScope lifetimeScope) : base(lifetimeScope)
    { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="server"></param>
    /// <param name="itemId"></param>
    /// <returns></returns>
    [HttpGet($"{{server}}/vending-{API_ENDPOINT}/{{itemId}}")]
    public async Task<IActionResult> GetVending(
        [FromRoute] string server,
        [FromRoute] int itemId)
        => await HandleQuery(new StoreItemValueSumaryQuery
        {
            ItemId = itemId,
            Server = server,
            StoreType = StoreItemValueSumaryQuery.EStoreItemStoreType.Vending
        }, server);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="server"></param>
    /// <param name="itemId"></param>
    /// <returns></returns>
    [HttpGet($"{{server}}/buying-{API_ENDPOINT}/{{itemId}}")]
    public async Task<IActionResult> GetBuying(
        [FromRoute] string server,
        [FromRoute] int itemId)
        => await HandleQuery(new StoreItemValueSumaryQuery
        {
            ItemId = itemId,
            Server = server,
            StoreType = StoreItemValueSumaryQuery.EStoreItemStoreType.Buying
        }, server);

}
