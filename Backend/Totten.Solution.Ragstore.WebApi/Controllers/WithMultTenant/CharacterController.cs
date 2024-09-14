namespace Totten.Solution.Ragstore.WebApi.Controllers.WithMultTenant;

using Autofac;
using Microsoft.AspNetCore.Mvc;
using Totten.Solution.Ragstore.ApplicationService.Features.Characters.Commands;
using Totten.Solution.Ragstore.WebApi.Bases;

/// <summary>
/// 
/// </summary>
[ApiController]
[Route("[controller]")]
public class CharacterController : BaseApiController
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="lifetimeScope"></param>
    public CharacterController(ILifetimeScope lifetimeScope) : base(lifetimeScope)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="server"></param>
    /// <param name="createCmd"></param>
    /// <returns></returns>
    [HttpPost("{server}")]
    public async Task<IActionResult> Post(
        [FromRoute] string server,
        [FromBody] CharacterCreateCommand createCmd)
            => await HandleCommand(createCmd, server);
}