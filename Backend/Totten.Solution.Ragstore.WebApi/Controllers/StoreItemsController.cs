namespace Totten.Solution.Ragstore.WebApi.Controllers;

using Autofac;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.ResponseModels;
using Totten.Solution.Ragstore.WebApi.Bases;

/// <summary>
/// 
/// </summary>
[ApiController]
[Route("[controller]")]
public class StoreItemsController : BaseApiController
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="lifetimeScope"></param>
    public StoreItemsController(ILifetimeScope lifetimeScope) : base(lifetimeScope)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="itemName"></param>
    /// <param name="server"></param>
    /// <param name="queryOptions"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetByName(
        [FromQuery] string itemName,
        [FromQuery] string server,
        ODataQueryOptions<StoreItemResponseModel> queryOptions)
    {
        return await HandleQueryable(new StoreItemsCollectionQuery
        {
            ItemName = itemName
        }, server, queryOptions);
    }
}
