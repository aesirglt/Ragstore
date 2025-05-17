namespace Totten.Solution.RagnaComercio.WebApi.Controllers.WithMultTenant;

using Autofac;
using FunctionalConcepts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Totten.Solution.RagnaComercio.ApplicationService.Features.Characters.Commands;
using Totten.Solution.RagnaComercio.WebApi.Bases;

/// <summary>
/// 
/// </summary>
/// <remarks>
/// 
/// </remarks>
/// <param name="lifetimeScope"></param>
[ApiController]
[Route("server/[controller]")]
[Authorize]
public class CharacterController(ILifetimeScope lifetimeScope) : BaseApiController(lifetimeScope)
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="createCmd"></param>
    /// <returns></returns>
    [HttpPost("{serverId}")]
    [ProducesResponseType<Success>(statusCode: 201)]
    public async Task<IActionResult> Post([FromRoute] Guid serverId, [FromBody] CharacterCreateCommand createCmd)
        => await HandleCommand(serverId, createCmd);
}