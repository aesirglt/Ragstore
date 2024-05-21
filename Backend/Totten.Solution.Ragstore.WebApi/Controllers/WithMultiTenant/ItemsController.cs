namespace Totten.Solution.Ragstore.WebApi.Controllers.WithMultiTenant;

using Autofac;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Totten.Solution.Ragstore.ApplicationService.Features.ItemsAggregation.Queries;
using Totten.Solution.Ragstore.WebApi.Bases;
using Totten.Solution.Ragstore.WebApi.ViewModels.Items;
/// <summary>
/// 
/// </summary>
[ApiController]
[Route("[controller]")]
public class ItemsController : BaseApiController
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="lifetimeScope"></param>
    public ItemsController(ILifetimeScope lifetimeScope) : base(lifetimeScope)
    {
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="server"></param>
    /// <param name="queryOptions"></param>
    /// <returns></returns>
    [HttpGet("{name}")]
    public async Task<IActionResult> GetByName(
        [FromRoute] string name,
        [FromQuery] string server,
        ODataQueryOptions<ItemResumeViewModel> queryOptions)
    {
        return await HandleQueryable(new ItemCollectionByNameQuery
        {
            Name = name
        }, server, queryOptions);
    }
}
