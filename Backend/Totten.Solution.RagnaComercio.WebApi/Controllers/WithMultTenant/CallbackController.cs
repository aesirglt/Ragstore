namespace Totten.Solution.RagnaComercio.WebApi.Controllers.WithMultTenant;

using Autofac;
using FunctionalConcepts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Totten.Solution.RagnaComercio.ApplicationService.Features.Callbacks.Commands;
using Totten.Solution.RagnaComercio.ApplicationService.Features.ItemsAggregation.Queries;
using Totten.Solution.RagnaComercio.ApplicationService.ViewModels.Callbacks;
using Totten.Solution.RagnaComercio.WebApi.Bases;
using Totten.Solution.RagnaComercio.WebApi.Dtos;
using Totten.Solution.RagnaComercio.WebApi.Dtos.Callbacks;

/// <summary>
/// Endpoint responsavel por notificações de items com baixos valores.
/// </summary>
/// <remarks>
/// 
/// </remarks>
/// <param name="lifetimeScope"></param>
[ApiController]
[Authorize]
public class CallbackController(ILifetimeScope lifetimeScope) : BaseApiController(lifetimeScope)
{

    /// <summary>
    /// Busca todas as notificações do servidor.
    /// </summary>
    /// <param name="serverId">Servidor</param>
    /// <param name="queryOptions">Filtro Dinamico</param>
    /// <returns></returns>
    [HttpGet("server/{serverId}/callbacks")]
    [ProducesResponseType<PaginationDto<CallbackResumeViewModel>>(statusCode: 200)]
    public async Task<IActionResult> Get([FromRoute] Guid serverId, ODataQueryOptions<CallbackResumeViewModel> queryOptions)
        => await HandleQueryable(new CallbackCollectionQuery(), queryOptions, serverId);

    /// <summary>
    /// Busca todas as notificações do usuário no servidor especificado.
    /// </summary>
    /// <param name="serverId">Servidor</param>
    /// <param name="queryOptions">Filtro Dinamico</param>
    /// <returns></returns>
    [HttpGet("server/{server}/callbacks-user")]
    [ProducesResponseType<PaginationDto<CallbackResumeViewModel>>(statusCode: 200)]
    public async Task<IActionResult> GetCallbackByUser([FromRoute] Guid serverId, ODataQueryOptions<CallbackResumeViewModel> queryOptions)
        => await HandleQueryable(new CallbackCollectionByUserIdQuery
        {
            UserId = base.UserId,
        }, queryOptions, serverId);

    /// <summary>
    /// Cria uma notificação no servidor especificado.
    /// </summary>
    /// <param name="serverId">Servidor</param>
    /// <param name="createDto">Objeto de criação de notificação</param>
    /// <returns></returns>
    [HttpPost("server/{serverId}/callbacks-items")]
    [ProducesResponseType<Success>(statusCode: 200)]
    public async Task<IActionResult> PostItems([FromRoute] Guid serverId, [FromBody] CallbackCreateDto createDto)
        => await HandleCommand(new CallbackSaveCommand
        {
            ItemId = createDto.ItemId,
            ItemPrice = createDto.ItemPrice,
            ServerId = serverId,
            UserId = base.UserId
        });

    /// <summary>
    /// 
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="callbackId"></param>
    /// <returns></returns>
    [HttpDelete("server/{serverId}/callbacks-items/{callbackId}")]
    [ProducesResponseType<Success>(statusCode: 200)]
    public async Task<IActionResult> Removetem([FromRoute] Guid serverId, [FromRoute] Guid callbackId)
        => await HandleCommand(serverId, new CallbackRemoveCommand
        {
            Id = callbackId,
            UserId = base.UserId
        });
}
