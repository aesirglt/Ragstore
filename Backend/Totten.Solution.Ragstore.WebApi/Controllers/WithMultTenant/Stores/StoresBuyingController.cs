namespace Totten.Solution.Ragstore.WebApi.Controllers.WithMultTenant.Stores;

using Autofac;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Commands;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.ResponseModels;
using Totten.Solution.Ragstore.ApplicationService.ViewModels.Stores;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Buyings;
using Totten.Solution.Ragstore.WebApi.Bases;

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
    [HttpGet($"{{server}}/{API_ENDPOINT}")]
    public async Task<IActionResult> GetAll(
        [FromRoute] string server, ODataQueryOptions<StoreResumeViewModel> queryOptions)
            => await HandleQueryable(new BuyingStoreCollectionQuery(), server, queryOptions);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="server"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet($"{{server}}/{API_ENDPOINT}/{{id}}")]
    public async Task<IActionResult> GetById(
        [FromRoute] string server,
        [FromRoute] int id)
            => await HandleQuery<BuyingStore, StoreDetailViewModel>(new BuyingStoreByIdQuery { Id = id }, server);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="server"></param>
    /// <param name="createCmd"></param>
    /// <returns></returns>
    [HttpPost($"{{server}}/{API_ENDPOINT}")]
    public async Task<IActionResult> Post(
        [FromRoute] string server, [FromBody] BuyingStoreSaveCommand createCmd)
            => await HandleCommand(createCmd, server);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="server"></param>
    /// <param name="createCmd"></param>
    /// <returns></returns>
    [HttpPost($"{{server}}/{API_ENDPOINT}-batch")]
    public async Task<IActionResult> PostBatch(
        [FromRoute] string server, [FromBody] BuyingStoreSaveCommand[] createCmd)
           => await HandleAccepted(server, createCmd);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="itemName"></param>
    /// <param name="server"></param>
    /// <param name="queryOptions"></param>
    /// <returns></returns>
    [HttpGet($"{{server}}/{API_ENDPOINT}/items")]
    public async Task<IActionResult> GetByName(
        [FromRoute] string server,
        [FromQuery] string? itemName,
        ODataQueryOptions<StoreItemResponseModel> queryOptions)
        => await HandleQueryable(new BuyingStoreItemsCollectionQuery
        {
            ItemName = itemName ?? string.Empty
        }, server, queryOptions);
}
