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
using Totten.Solution.RagnaComercio.WebApi.Dtos;
using Totten.Solution.RagnaComercio.WebApi.Dtos.Agents;
using Totten.Solution.RagnaComercio.WebApi.Filters;

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
    /// <param name="serverId"></param>
    /// <param name="createCmd"></param>
    /// <returns></returns>
    [HttpPost("server/{serverId}/agents")]
    [ProducesResponseType<Success>(statusCode: 201)]
    [CustomAuthorizeAttributte(RoleLevelEnum.admin | RoleLevelEnum.system)]
    public async Task<IActionResult> Post(
        [FromRoute] Guid serverId,
        [FromBody] AgentCreateDto createCmd)
            => await HandleCommand(new AgentCreateCommand
            {
                ServerId = serverId,
                Name = createCmd.Name,
            });

    /// <summary>
    /// Busca todos os agentes de um servidor.
    /// </summary>
    /// <param name="serverId">Servidor</param>
    /// <param name="queryOptions"></param>
    /// <returns></returns>
    [HttpGet("server/{serverId}/agents")]
    [ProducesResponseType<PaginationDto<AgentResumeViewModel>>(statusCode: 200)]
    [CustomAuthorizeAttributte(RoleLevelEnum.admin | RoleLevelEnum.system)]
    public async Task<IActionResult> GetAll(
        [FromRoute] Guid serverId,
        ODataQueryOptions<AgentResumeViewModel> queryOptions)
        => await HandleQueryable(new AgentCollectionQuery { ServerId = serverId }, queryOptions);
}
