namespace Totten.Solution.Ragstore.WebApi.Controllers.WithMultTenant.Stores;

using Autofac;
using Microsoft.AspNetCore.Mvc;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries;
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
    const string API_ENDPOINT = "store-sumary";

    /// <summary>
    /// 
    /// </summary>
    /// <param name="server"></param>
    /// <param name="itemId"></param>
    /// <returns></returns>
    [HttpGet($"{{server}}/vending-{API_ENDPOINT}/{{itemId}}")]
    [ProducesResponseType<StoreItemValueSumaryResponseModel>(statusCode: 200)]
    public async Task<IActionResult> GetVending(
        [FromRoute] string server,
        [FromRoute] int itemId)
        => await HandleQuery(new StoreItemValueSumaryQuery
        {
            ItemId = itemId,
            StoreType = StoreItemValueSumaryQuery.EStoreItemStoreType.Vending
        }, server);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="server"></param>
    /// <param name="itemId"></param>
    /// <returns></returns>
    [HttpGet($"{{server}}/buying-{API_ENDPOINT}/{{itemId}}")]
    [ProducesResponseType<StoreItemValueSumaryResponseModel>(statusCode: 200)]
    public async Task<IActionResult> GetBuying(
        [FromRoute] string server,
        [FromRoute] int itemId)
        => await HandleQuery(new StoreItemValueSumaryQuery
        {
            ItemId = itemId,
            StoreType = StoreItemValueSumaryQuery.EStoreItemStoreType.Buying
        }, server);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="server"></param>
    /// <returns></returns>
    [HttpGet($"{{server}}/{API_ENDPOINT}/searched-items")]
    [ProducesResponseType<SearchedItemViewModel>(statusCode: 200)]
    public async Task<IActionResult> GetSearched(
        [FromRoute] string server)
        => await HandleQuery(new SearchedItemSumaryQuery(), server);

}
