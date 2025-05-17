namespace Totten.Solution.RagnaComercio.WebApi.Controllers.WithMultTenant.Stores;

using Autofac;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.Commands;
using Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.Queries;
using Totten.Solution.RagnaComercio.ApplicationService.ViewModels.Stores;
using Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Vendings;
using Totten.Solution.RagnaComercio.WebApi.Bases;
using Totten.Solution.RagnaComercio.WebApi.Dtos;
using Totten.Solution.RagnaComercio.WebApi.Dtos.Stores;

/// <summary>
/// 
/// </summary>
/// <remarks>
/// 
/// </remarks>
/// <param name="lifetimeScope"></param>
[ApiController]
public class StoresController(ILifetimeScope lifetimeScope) : BaseApiController(lifetimeScope)
{
    const string API_ENDPOINT = "stores";

    /// <summary>
    /// 
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="storeType"></param>
    /// <param name="queryOptions"></param>
    /// <returns></returns>
    [HttpGet($"server/{{serverId}}/{API_ENDPOINT}")]
    [ProducesResponseType<PaginationDto<StoreResumeViewModel>>(statusCode: 200)]
    public async Task<IActionResult> GetAll(
        [FromRoute] Guid serverId,
        [FromQuery] string? storeType,
        ODataQueryOptions<StoreResumeViewModel> queryOptions)
            => await HandleQueryable(new StoreItemCollectionQuery
            {
                StoreType = storeType ?? nameof(VendingStore),
            }, queryOptions, serverId);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpPost($"server/{{serverId}}/{API_ENDPOINT}-vending")]
    [ProducesResponseType<PaginationDto<StoreResumeViewModel>>(statusCode: 200)]
    public async Task<IActionResult> Create([FromRoute] Guid serverId, [FromBody] VendingStoreSaveDto dto)
            => await HandleCommand(serverId, _mapper.Map<VendingStoreSaveCommand>(dto));

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpPost($"server/{{serverId}}/{API_ENDPOINT}-buying")]
    [ProducesResponseType<PaginationDto<StoreResumeViewModel>>(statusCode: 200)]
    public async Task<IActionResult> Create([FromRoute] Guid serverId, [FromBody] BuyingStoreSaveDto dto)
            => await HandleCommand(serverId, _mapper.Map<BuyingStoreSaveCommand>(dto));
}
