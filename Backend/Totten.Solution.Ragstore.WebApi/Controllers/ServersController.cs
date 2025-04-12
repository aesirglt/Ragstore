namespace Totten.Solution.Ragstore.WebApi.Controllers;

using Autofac;
using FunctionalConcepts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Totten.Solution.Ragstore.ApplicationService.Features.Servers.Commands;
using Totten.Solution.Ragstore.ApplicationService.Features.Servers.Queries;
using Totten.Solution.Ragstore.ApplicationService.ViewModels.Servers;
using Totten.Solution.Ragstore.Domain.Features.CallbackAggregation;
using Totten.Solution.Ragstore.Domain.Features.Servers;
using Totten.Solution.Ragstore.WebApi.Bases;

/// <summary>
/// Endpoint responsavel por servidores.
/// </summary>
/// <remarks>
/// 
/// </remarks>
/// <param name="lifetimeScope"></param>
[ApiController]
public class ServersController(ILifetimeScope lifetimeScope) : BaseApiController(lifetimeScope)
{

    /// <summary>
    /// Cria um novo servidor que será monitorado pela api.
    /// </summary>
    /// <param name="createCmd">Objeto de criação com dados do servidor</param>
    /// <returns></returns>
    [HttpPost("servers")]
    [ProducesResponseType<Success>(statusCode: 201)]
    public async Task<IActionResult> Post([FromBody] ServerCreateCommand createCmd)
            => await HandleCommand(createCmd);

    /// <summary>
    /// Busca todos os servidores cadastrados no sistema.
    /// </summary>
    /// <param name="queryOptions">Filtro dinamico</param>
    /// <returns></returns>
    [HttpGet("servers")]
    [ProducesResponseType<IQueryable<ServerResume>>(statusCode: 200)]
    public async Task<IActionResult> GetAll(ODataQueryOptions<ServerResume> queryOptions)
        => await HandleQueryable(new ServerCollectionQuery(), queryOptions);
    
    /// <summary>
    /// Busca ultima atualização do servidor especificado.
    /// </summary>
    /// <param name="serverName">Servidor</param>
    /// <returns></returns>
    [HttpGet("servers/{serverName}")]
    [ProducesResponseType<IQueryable<ServerVerifyDTO>>(statusCode: 200)]
    public async Task<IActionResult> GetAll([FromRoute] string serverName)
        => await HandleQuery<Server, ServerVerifyDTO>(new ServerByNameQuery { Name = serverName });
}
