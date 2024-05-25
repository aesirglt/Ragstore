namespace Totten.Solution.Ragstore.WebApi.Controllers;

using Autofac;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Totten.Solution.Ragstore.ApplicationService.Features.Servers.Commands;
using Totten.Solution.Ragstore.ApplicationService.Features.Servers.Queries;
using Totten.Solution.Ragstore.Domain.Features.Servers;
using Totten.Solution.Ragstore.WebApi.Bases;
using Totten.Solution.Ragstore.ApplicationService.ViewModels.Servers;

/// <summary>
/// 
/// </summary>
[ApiController]
public class ServersController : BaseApiController
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="lifetimeScope"></param>
    public ServersController(ILifetimeScope lifetimeScope) : base(lifetimeScope) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="createCmd"></param>
    /// <returns></returns>
    [HttpPost("servers")]
    public async Task<IActionResult> Post([FromBody] ServerCreateCommand createCmd)
            => await HandleCommand(createCmd);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="queryOptions"></param>
    /// <returns></returns>
    [HttpGet("servers")]
    [ProducesResponseType<IQueryable<Server>>(statusCode: 200)]
    public async Task<IActionResult> GetAll(ODataQueryOptions<ServerResume> queryOptions)
        => await HandleQueryable(new ServerCollectionQuery(), queryOptions);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="serverName"></param>
    /// <returns></returns>
    [HttpGet("servers/{serverName}")]
    public async Task<IActionResult> GetAll([FromRoute] string serverName)
        => await HandleQuery<Server, ServerVerifyDTO>(new ServerByNameQuery { Name = serverName });
}
