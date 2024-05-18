namespace Totten.Solution.Ragstore.WebApi.Controllers.Stores;

using Autofac;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Commands;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.ResponseModels;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Buyings;
using Totten.Solution.Ragstore.WebApi.Bases;
using Totten.Solution.Ragstore.WebApi.ViewModels.Stores;
/// <summary>
/// 
/// </summary>
[ApiController]
public class StoresBuyingController : BaseApiController
{
    const string API_ENDPOINT = "stores-buying";
    /// <summary>
    /// 
    /// </summary>
    /// <param name="lifetimeScope"></param>
    public StoresBuyingController(ILifetimeScope lifetimeScope) : base(lifetimeScope)
    {
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="server"></param>
    /// <param name="queryOptions"></param>
    /// <returns></returns>
    [HttpGet(API_ENDPOINT)]
    public async Task<IActionResult> GetAll(
        [FromQuery] string server,
        ODataQueryOptions<StoreResumeViewModel> queryOptions)
            => await HandleQueryable(new BuyingStoreCollectionQuery(), server, queryOptions);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="server"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet($"{API_ENDPOINT}/{{id}}")]
    public async Task<IActionResult> GetById(
        [FromQuery] string server,
        [FromRoute] int id)
            => await HandleQuery<BuyingStore, StoreDetailViewModel>(
                        new BuyingStoreByIdQuery { Id = id },
                        server);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="server"></param>
    /// <param name="createCmd"></param>
    /// <returns></returns>
    [HttpPost(API_ENDPOINT)]
    public async Task<IActionResult> Post(
        [FromQuery] string server,
        [FromBody] BuyingStoreSaveCommand createCmd)
            => await HandleCommand(createCmd, server);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="server"></param>
    /// <param name="createCmd"></param>
    /// <returns></returns>
    [HttpPost($"{API_ENDPOINT}-batch")]
    public async Task<IActionResult> PostBatch(
        [FromQuery] string server,
        [FromBody] BuyingStoreSaveCommand[] createCmd)
           => await HandleAccepted(server, createCmd);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="itemName"></param>
    /// <param name="server"></param>
    /// <param name="queryOptions"></param>
    /// <returns></returns>
    [HttpGet("stores-buying/items")]
    public async Task<IActionResult> GetByName(
        [FromQuery] string? itemName,
        [FromQuery] string server,
        ODataQueryOptions<StoreItemResponseModel> queryOptions)
    {
        return await HandleQueryable(new BuyingStoreItemsCollectionQuery
        {
            ItemName = itemName
        }, server, queryOptions);
    }
}
