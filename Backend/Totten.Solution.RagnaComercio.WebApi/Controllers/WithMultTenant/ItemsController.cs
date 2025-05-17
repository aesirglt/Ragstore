namespace Totten.Solution.RagnaComercio.WebApi.Controllers.WithMultTenant;

using Autofac;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Totten.Solution.RagnaComercio.ApplicationService.Features.ItemsAggregation.Queries;
using Totten.Solution.RagnaComercio.ApplicationService.Features.ItemsAggregation.ResponseModels;
using Totten.Solution.RagnaComercio.ApplicationService.ViewModels.Items;
using Totten.Solution.RagnaComercio.Domain.Features.Servers;
using Totten.Solution.RagnaComercio.WebApi.Bases;
using Totten.Solution.RagnaComercio.WebApi.Dtos;

/// <summary>
/// Enpoint responsavel por itens dentro do jogo
/// </summary>
/// <remarks>
/// 
/// </remarks>
/// <param name="lifetimeScope"></param>
[ApiController]
public class ItemsController(ILifetimeScope lifetimeScope) : BaseApiController(lifetimeScope)
{
    /// <summary>
    /// Busca um item com base em seu nome.
    /// </summary>
    /// <param name="name">Nome do item</param>
    /// <param name="serverId">Servidor</param>
    /// <param name="queryOptions"></param>
    /// <returns></returns>
    [HttpGet("{serverId}/items-name")]
    [ProducesResponseType<PaginationDto<ItemResumeViewModel>>(statusCode: 200)]
    public async Task<IActionResult> GetByName(
        [FromRoute] Guid serverId,
        [FromQuery] string name,
        ODataQueryOptions<ItemResumeViewModel> queryOptions)
        => await HandleQueryable(new ItemCollectionByNameQuery
        {
            Name = name
        }, queryOptions, serverId);

    /// <summary>
    /// busca um item com base em seu id.
    /// </summary>
    /// <param name="itemId">Identificador</param>
    /// <param name="serverId">Servidor</param>
    /// <returns></returns>
    [HttpGet("{serverId}/items/{itemId}")]
    [ProducesResponseType<ItemDetailResponseModel>(statusCode: 200)]
    public async Task<IActionResult> GetByName(
        [FromRoute] int itemId,
        [FromRoute] Guid serverId)
    {
        return await HandleQuery(new ItemByIdQuery
        {
            ItemId = itemId,
            Server = "",
            ServerLanguage = "pt-BR"
        }, serverId);
    }
}
