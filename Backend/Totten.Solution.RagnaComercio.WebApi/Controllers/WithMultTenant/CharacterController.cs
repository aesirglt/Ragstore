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
[Route("[controller]")]
[Authorize]
public class CharacterController(ILifetimeScope lifetimeScope) : BaseApiController(lifetimeScope)
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="server"></param>
    /// <param name="createCmd"></param>
    /// <returns></returns>
    [HttpPost("{server}")]
    [ProducesResponseType<Success>(statusCode: 201)]
    public async Task<IActionResult> Post(
        [FromRoute] string server,
        [FromBody] CharacterCreateCommand createCmd)
            => await HandleCommand(createCmd, server);
}