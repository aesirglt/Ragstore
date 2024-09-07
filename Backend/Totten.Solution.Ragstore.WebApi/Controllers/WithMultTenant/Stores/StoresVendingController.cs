namespace Totten.Solution.Ragstore.WebApi.Controllers.WithMultTenant.Stores;

using Autofac;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Commands;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.ResponseModels;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;
using Totten.Solution.Ragstore.WebApi.Bases;
using Totten.Solution.Ragstore.ApplicationService.ViewModels.Stores;
/// <summary>
/// 
/// </summary>
[ApiController]
public class StoresVendingController : BaseApiController
{
    const string API_ENDPOINT = "stores-vending";
    /// <summary>
    /// 
    /// </summary>
    /// <param name="lifetimeScope"></param>
    public StoresVendingController(ILifetimeScope lifetimeScope) : base(lifetimeScope)
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
        [FromRoute] string server,
        ODataQueryOptions<StoreResumeViewModel> queryOptions)
            => await HandleQueryable(new VendingStoreCollectionQuery(), server, queryOptions);
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
            => await HandleQuery<VendingStore, StoreDetailViewModel>(
                        new VendingStoreByIdQuery { Id = id },
                        server);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="server"></param>
    /// <param name="createCmd"></param>
    /// <returns></returns>
    [HttpPost($"{{server}}/{API_ENDPOINT}")]
    public async Task<IActionResult> Post(
        [FromRoute] string server,
        [FromBody] VendingStoreSaveCommand createCmd)
            => await HandleCommand(createCmd, server);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="server"></param>
    /// <param name="createCmd"></param>
    /// <returns></returns>
    [HttpPost($"{{server}}/{API_ENDPOINT}-batch")]
    public async Task<IActionResult> PostBatch(
        [FromRoute] string server,
        [FromBody] VendingStoreSaveCommand[] createCmd)
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
    {
        return await HandleQueryable(new VendingStoreItemsCollectionQuery
        {
            ItemName = itemName ?? string.Empty
        }, server, queryOptions);
    }
}
