namespace Totten.Solution.Ragstore.WebApi.Controllers.WithMultTenant;

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
    [HttpGet("{server}/items-name/{name}")]
    public async Task<IActionResult> GetByName(
        [FromRoute] string name,
        [FromRoute] string server,
        ODataQueryOptions<ItemResumeViewModel> queryOptions)
        => await HandleQueryable(new ItemCollectionByNameQuery
        {
            Name = name
        }, server, queryOptions);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="itemId"></param>
    /// <param name="server"></param>
    /// <returns></returns>
    [HttpGet("{server}/items/{itemId}")]
    public async Task<IActionResult> GetByName(
        [FromRoute] int itemId,
        [FromRoute] string server)
    {
        return await HandleQuery(new ItemByIdQuery
        {
            ItemId = itemId,
            Server = server,
            ServerLanguage = "pt-BR"
        }, server);
    }
}
