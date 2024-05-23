namespace Totten.Solution.Ragstore.WebApi.Controllers.WithMultTenant;

using Autofac;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Totten.Solution.Ragstore.ApplicationService.Features.Callbacks.Commands;
using Totten.Solution.Ragstore.ApplicationService.Features.ItemsAggregation.Queries;
using Totten.Solution.Ragstore.Domain.Features.CallbackAggregation;
using Totten.Solution.Ragstore.Infra.Cross.CrossDTOs;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;
using Totten.Solution.Ragstore.WebApi.Bases;
using Totten.Solution.Ragstore.WebApi.Dtos.Callbacks;
using Totten.Solution.Ragstore.WebApi.ViewModels.Items;

/// <summary>
/// 
/// </summary>
/// 
[ApiController]
public class CallbackController : BaseApiController
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="lifetimeScope"></param>
    public CallbackController(ILifetimeScope lifetimeScope) : base(lifetimeScope)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="server"></param>
    /// <param name="queryOptions"></param>
    /// <returns></returns>
    [HttpGet("callbacks")]
    [ProducesResponseType<IQueryable<ItemResumeViewModel>>(statusCode: 200)]
    public async Task<IActionResult> Get([FromQuery] string server, ODataQueryOptions<ItemResumeViewModel> queryOptions)
        => await HandleQueryable(new CallbackCollectionQuery(), server, queryOptions);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="server"></param>
    /// <param name="queryOptions"></param>
    /// <returns></returns>
    [HttpGet("callbacks-user")]
    [ProducesResponseType<IQueryable<Callback>>(statusCode: 200)]
    public async Task<IActionResult> GetUsers([FromQuery] string server, ODataQueryOptions<Callback> queryOptions)
        => await HandleQueryable(new CallbackCollectionByUserIdQuery
        {
            UserId = "d7aeb595-44a5-4f5d-822e-980f35ace12d"
        }, server, queryOptions);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="server"></param>
    /// <param name="createDto"></param>
    /// <returns></returns>
    [HttpPost("callbacks-items")]
    [ProducesResponseType<Unit>(statusCode: 200)]
    public async Task<IActionResult> PostItems([FromQuery] string server, [FromBody] CallbackCreateDto createDto)
        => await HandleCommand(_mapper.Map<CallbackSaveCommand>((createDto, new UserData
        {
            Id = $"d7aeb595-44a5-4f5d-822e-980f35ace12d",
            Email = "aleffmds@gmail.com",
            Cellphone = "+5584988633251",
            Level = EUserLevel.SYSTEM
        })), server);
}
