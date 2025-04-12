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
using FunctionalConcepts;
using Totten.Solution.Ragstore.WebApi.Dtos.Stores;
using Totten.Solution.Ragstore.Infra.Cross.Statics;

/// <summary>
/// 
/// </summary>
/// <remarks>
/// 
/// </remarks>
/// <param name="lifetimeScope"></param>
[ApiController]
public class StoresVendingController(ILifetimeScope lifetimeScope) : BaseApiController(lifetimeScope)
{
    const string API_ENDPOINT = "stores-vending";

    /// <summary>
    /// 
    /// </summary>
    /// <param name="server"></param>
    /// <param name="queryOptions"></param>
    /// <returns></returns>
    [HttpGet($"{{server}}/{API_ENDPOINT}")]
    [ProducesResponseType<IQueryable<StoreResumeViewModel>>(statusCode: 200)]
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
    [ProducesResponseType<StoreDetailViewModel>(statusCode: 200)]
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
    /// <param name="createCmdDto"></param>
    /// <returns></returns>
    [HttpPost($"{{server}}/{API_ENDPOINT}")]
    [ProducesResponseType<Success>(statusCode: 201)]
    public async Task<IActionResult> Post(
        [FromRoute] string server,
        [FromBody] VendingStoreSaveDto createCmdDto)
            => await HandleCommand(_mapper.Map<VendingStoreSaveCommand>(createCmdDto).Apply(cmd =>
            {
                cmd.Server = server;
                return cmd;
            }), server);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="server"></param>
    /// <param name="createCmdDto"></param>
    /// <returns></returns>
    [HttpPost($"{{server}}/{API_ENDPOINT}-batch")]
    [ProducesResponseType<Success>(statusCode: 201)]
    public async Task<IActionResult> PostBatch(
        [FromRoute] string server,
        [FromBody] VendingStoreSaveDto[] createCmdDto)
           => await HandleAccepted(server, [.. createCmdDto.Select(cmd =>  _mapper.Map<VendingStoreSaveCommand>(cmd).Apply(cmd =>
           {
               cmd.Server = server;
               return cmd;
           }))]);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="itemName"></param>
    /// <param name="server"></param>
    /// <param name="queryOptions"></param>
    /// <returns></returns>
    [HttpGet($"{{server}}/{API_ENDPOINT}/items")]
    [ProducesResponseType<StoreItemResponseModel>(statusCode: 200)]
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
