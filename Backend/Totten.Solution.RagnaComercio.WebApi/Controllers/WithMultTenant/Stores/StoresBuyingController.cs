namespace Totten.Solution.RagnaComercio.WebApi.Controllers.WithMultTenant.Stores;

using Autofac;
using FunctionalConcepts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.Commands;
using Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.Queries.Buyings;
using Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.ResponseModels;
using Totten.Solution.RagnaComercio.ApplicationService.ViewModels.Stores;
using Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Buyings;
using Totten.Solution.RagnaComercio.Infra.Cross.Statics;
using Totten.Solution.RagnaComercio.WebApi.Bases;
using Totten.Solution.RagnaComercio.WebApi.Dtos.Stores;

/// <summary>
/// 
/// </summary>
/// <remarks>
/// 
/// </remarks>
/// <param name="lifetimeScope"></param>
[ApiController]
public class StoresBuyingController(ILifetimeScope lifetimeScope) : BaseApiController(lifetimeScope)
{
    const string API_ENDPOINT = "stores-buying";

    /// <summary>
    /// 
    /// </summary>
    /// <param name="server"></param>
    /// <param name="queryOptions"></param>
    /// <returns></returns>
    [HttpGet($"{{server}}/{API_ENDPOINT}")]
    [ProducesResponseType<IQueryable<StoreResumeViewModel>>(statusCode: 200)]
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
    [ProducesResponseType<StoreDetailViewModel>(statusCode: 200)]
    public async Task<IActionResult> GetById(
        [FromRoute] string server,
        [FromRoute] int id)
            => await HandleQuery<BuyingStore, StoreDetailViewModel>(new BuyingStoreByIdQuery { Id = id }, server);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="server"></param>
    /// <param name="createCmdDto"></param>
    /// <returns></returns>
    [HttpPost($"{{server}}/{API_ENDPOINT}")]
    [ProducesResponseType<Success>(statusCode: 201)]
    [Authorize]
    public async Task<IActionResult> Post(
        [FromRoute] string server,
        [FromBody] BuyingStoreSaveDto createCmdDto)
            => await HandleCommand(_mapper.Map<BuyingStoreSaveCommand>(createCmdDto).Apply(cmd =>
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
    [ProducesResponseType<AcceptedResult>(statusCode: 202)]
    [Authorize]
    public async Task<IActionResult> PostBatch(
        [FromRoute] string server,
        [FromBody] BuyingStoreSaveDto[] createCmdDto)
           => await HandleAccepted(server, [.. createCmdDto.Select(cmd =>  _mapper.Map<BuyingStoreSaveCommand>(cmd).Apply(cmd =>
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
    [ProducesResponseType<IQueryable<StoreItemResponseModel>>(statusCode: 200)]
    public async Task<IActionResult> GetByName(
        [FromRoute] string server,
        [FromQuery] string? itemName,
        ODataQueryOptions<StoreItemResponseModel> queryOptions)
        => await HandleQueryable(new BuyingStoreItemsCollectionQuery
        {
            ItemName = itemName ?? string.Empty
        }, server, queryOptions);
}
