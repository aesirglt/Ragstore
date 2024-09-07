namespace Totten.Solution.Ragstore.WebApi.Controllers.WithMultTenant;

using Autofac;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Totten.Solution.Ragstore.ApplicationService.Features.ItemsAggregation.Queries;
using Totten.Solution.Ragstore.WebApi.Bases;
using Totten.Solution.Ragstore.ApplicationService.ViewModels.Items;
/// <summary>
/// Enpoint responsavel por itens dentro do jogo
/// </summary>
[ApiController]
public class ItemsController : BaseApiController
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="lifetimeScope"></param>
    public ItemsController(ILifetimeScope lifetimeScope) : base(lifetimeScope)
    {
    }
    /// <summary>
    /// Busca um item com base em seu nome.
    /// </summary>
    /// <param name="name">Nome do item</param>
    /// <param name="server">Servidor</param>
    /// <param name="queryOptions"></param>
    /// <returns></returns>
    [HttpGet("{server}/items-name/{name}")]
    public async Task<IActionResult> GetByName(
        [FromRoute] string name,
        [FromRoute] string server,
        ODataQueryOptions<ItemResumeViewModel> queryOptions)
        => await HandleQueryable(new ItemCollectionByNameQuery
        {
            Name = name
        }, server, queryOptions);

    /// <summary>
    /// busca um item com base em seu id.
    /// </summary>
    /// <param name="itemId">Identificador</param>
    /// <param name="server">Servidor</param>
    /// <returns></returns>
    [HttpGet("{server}/items/{itemId}")]
    public async Task<IActionResult> GetByName(
        [FromRoute] int itemId,
        [FromRoute] string server)
    {
        return await HandleQuery(new ItemByIdQuery
        {
            ItemId = itemId,
            Server = server,
            ServerLanguage = "pt-BR"
        }, server);
    }
}
