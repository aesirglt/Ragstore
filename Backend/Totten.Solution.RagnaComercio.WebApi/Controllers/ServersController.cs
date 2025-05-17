namespace Totten.Solution.RagnaComercio.WebApi.Controllers;

using Autofac;
using FunctionalConcepts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Totten.Solution.RagnaComercio.ApplicationService.Features.Servers.Commands;
using Totten.Solution.RagnaComercio.ApplicationService.Features.Servers.Queries;
using Totten.Solution.RagnaComercio.ApplicationService.ViewModels.Servers;
using Totten.Solution.RagnaComercio.WebApi.Bases;
using Totten.Solution.RagnaComercio.WebApi.Dtos;
using Totten.Solution.RagnaComercio.WebApi.Filters;

/// <summary>
/// Endpoint responsavel por servidores.
/// </summary>
/// <remarks>
/// 
/// </remarks>
/// <param name="lifetimeScope"></param>
[ApiController]
[Route("[Controller]")]
public class ServersController(ILifetimeScope lifetimeScope) : BaseApiController(lifetimeScope)
{

    /// <summary>
    /// Cria um novo servidor que será monitorado pela api.
    /// </summary>
    /// <param name="createCmd">Objeto de criação com dados do servidor</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType<Success>(statusCode: 201)]
    [ProducesResponseType<ProblemDetails>(statusCode: 400)]
    [CustomAuthorizeAttributte(RoleLevelEnum.admin | RoleLevelEnum.system)]
    public async Task<IActionResult> Post([FromBody] ServerCreateCommand createCmd)
            => await HandleCommand(createCmd);

    /// <summary>
    /// Busca todos os servidores cadastrados no sistema.
    /// </summary>
    /// <param name="queryOptions">Filtro dinamico</param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType<PaginationDto<ServerResume>>(statusCode: 200)]
    [ProducesResponseType<ProblemDetails>(statusCode: 400)]
    [ProducesResponseType<ProblemDetails>(statusCode: 500)]
    public async Task<IActionResult> GetAll(ODataQueryOptions<ServerResume> queryOptions)
        => await HandleQueryable(new ServerCollectionQuery(), queryOptions);
}
