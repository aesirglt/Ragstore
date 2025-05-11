namespace Totten.Solution.RagnaComercio.WebApi.Controllers.WithMultTenant.Stores;

using Autofac;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.Queries;
using Totten.Solution.RagnaComercio.ApplicationService.ViewModels.Stores;
using Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Vendings;
using Totten.Solution.RagnaComercio.WebApi.Bases;

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
    /// <param name="server"></param>
    /// <param name="storeType"></param>
    /// <param name="queryOptions"></param>
    /// <returns></returns>
    [HttpGet($"{{server}}/{API_ENDPOINT}")]
    [ProducesResponseType<IQueryable<StoreResumeViewModel>>(statusCode: 200)]
    public async Task<IActionResult> GetAll(
        [FromRoute] string server,
        [FromQuery] string? storeType,
        ODataQueryOptions<StoreResumeViewModel> queryOptions)
            => await HandleQueryable(new StoreItemCollectionQuery
            {
                StoreType = storeType ?? nameof(VendingStore),
            }, server, queryOptions);
}
