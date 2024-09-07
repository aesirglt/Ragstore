namespace Totten.Solution.Ragstore.WebApi.Controllers.WithMultTenant;

using Autofac;
using Microsoft.AspNetCore.Mvc;
using Totten.Solution.Ragstore.ApplicationService.Features.Agents.Commands;
using Totten.Solution.Ragstore.WebApi.Bases;

/// <summary>
/// Endpoint responsavel por clients que enviam informações dos servidores
/// Esses clients podem ser envios manuais ou automaticos.
/// </summary>
[ApiController]
[Route("[controller]")]
public class AgentsController : BaseApiController
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="lifetimeScope"></param>
    public AgentsController(ILifetimeScope lifetimeScope) : base(lifetimeScope)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="createCmd"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AgentCreateCommand createCmd)
            => await HandleCommand(createCmd, createCmd.Server);
}
