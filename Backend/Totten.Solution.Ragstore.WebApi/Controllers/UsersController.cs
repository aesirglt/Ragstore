namespace Totten.Solution.Ragstore.WebApi.Controllers;

using Autofac;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Totten.Solution.Ragstore.ApplicationService.Features.ItemsAggregation.Queries;
using Totten.Solution.Ragstore.Domain.Features.Callbacks;
using Totten.Solution.Ragstore.WebApi.Bases;
using Totten.Solution.Ragstore.WebApi.ViewModels.Items;

/// <summary>
/// 
/// </summary>
[ApiController]
[Route("[controller]")]
public class UsersController : BaseApiController
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="lifetimeScope"></param>
    public UsersController(ILifetimeScope lifetimeScope) : base(lifetimeScope)
    {
    }

    [HttpGet()]
    public async Task<IActionResult> Get(
        [FromQuery] string server,
        ODataQueryOptions<Callback> queryOptions)
    {
        return await HandleQueryable(new CallbackCollectionByUserIdQuery
        {
            UserId = "d7aeb595-44a5-4f5d-822e-980f35ace12d"
        }, server, queryOptions);
    }
}
