namespace Totten.Solution.Ragstore.WebApi.Controllers;

using Autofac;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Totten.Solution.Ragstore.ApplicationService.Features.ItemsAggregation.Queries;
using Totten.Solution.Ragstore.Domain.Features.CallbackAggregation;
using Totten.Solution.Ragstore.WebApi.Bases;

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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="server">servidor que será feita a consulta</param>
    /// <param name="queryOptions">filtros de odata para retornos</param>
    /// <returns></returns>
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
