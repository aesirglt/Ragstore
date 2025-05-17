namespace Totten.Solution.RagnaComercio.WebApi.Controllers.WithMultTenant.Stores;

using Autofac;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.Queries;
using Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.ResponseModels;
using Totten.Solution.RagnaComercio.ApplicationService.ViewModels.Stores;
using Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Vendings;
using Totten.Solution.RagnaComercio.WebApi.Bases;
using Totten.Solution.RagnaComercio.WebApi.Dtos;

/// <summary>
/// 
/// </summary>
/// <remarks>
/// 
/// </remarks>
/// <param name="lifetimeScope"></param>
[ApiController]
public class StoreSummaryController(ILifetimeScope lifetimeScope) : BaseApiController(lifetimeScope)
{
    const string API_ENDPOINT = "store-summary";

    /// <summary>
    /// 
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="itemId"></param>
    /// <param name="storeType"></param>
    /// <returns></returns>
    [HttpGet($"{{serverId}}/{API_ENDPOINT}/{{itemId}}")]
    [ProducesResponseType<StoreItemValueSumaryResponseModel>(statusCode: 200)]
    public async Task<IActionResult> GetVending(
        [FromRoute] Guid serverId,
        [FromQuery] string? storeType,
        [FromRoute] int itemId)
        => await HandleQuery(new StoreItemValueSumaryQuery
        {
            ItemId = itemId,
            StoreType = storeType ?? nameof(VendingStore),
        }, serverId);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="queryOptions"></param>
    /// <returns></returns>
    [HttpGet($"{{serverId}}/{API_ENDPOINT}/searched-items")]
    [ProducesResponseType<PaginationDto<SearchedItemViewModel>>(statusCode: 200)]
    public async Task<IActionResult> GetSearched(
        [FromRoute] Guid serverId,
        ODataQueryOptions<SearchedItemViewModel> queryOptions)
        => await HandleQueryable(new SearchedItemSumaryQuery(), queryOptions, serverId);
}
