namespace Totten.Solution.RagnaComercio.WebApi.Controllers.WithMultTenant;

using Autofac;
using FunctionalConcepts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Totten.Solution.RagnaComercio.ApplicationService.Features.Agents.Commands;
using Totten.Solution.RagnaComercio.ApplicationService.Features.Agents.Queries;
using Totten.Solution.RagnaComercio.ApplicationService.ViewModels.Agents;
using Totten.Solution.RagnaComercio.WebApi.Bases;
using Totten.Solution.RagnaComercio.WebApi.Dtos.Agents;

/// <summary>
/// Endpoint responsavel por clients que enviam informações dos servidores
/// Esses clients podem ser envios manuais ou automaticos.
/// </summary>
/// <remarks>
/// 
/// </remarks>
/// <param name="lifetimeScope"></param>
[ApiController]
public class AgentsController(ILifetimeScope lifetimeScope) : BaseApiController(lifetimeScope)
{

    /// <summary>
    /// 
    /// </summary>
    /// <param name="server"></param>
    /// <param name="createCmd"></param>
    /// <returns></returns>
    [HttpPost("{server}/agents")]
    [ProducesResponseType<Success>(statusCode: 201)]
    [Authorize]
    public async Task<IActionResult> Post(
        [FromRoute] string server,
        [FromBody] AgentCreateDto createCmd)
            => await HandleCommand(serverId => new AgentCreateCommand
            {
                ServerId = serverId,
                Name = createCmd.Name,
            }, server);

    /// <summary>
    /// Busca todos os agentes de um servidor.
    /// </summary>
    /// <param name="server">Servidor</param>
    /// <param name="queryOptions"></param>
    /// <returns></returns>
    [HttpGet("{server}/agents")]
    [ProducesResponseType<IQueryable<AgentResumeViewModel>>(statusCode: 200)]
    [Authorize]
    public async Task<IActionResult> GetAll(
        [FromRoute] string server,
        ODataQueryOptions<AgentResumeViewModel> queryOptions)
        => await HandleQueryable(serverId => new AgentCollectionQuery { ServerId = serverId }, server, queryOptions);
}
