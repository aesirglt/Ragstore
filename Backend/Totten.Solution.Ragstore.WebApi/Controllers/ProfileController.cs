namespace Totten.Solution.Ragstore.WebApi.Controllers;

using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Totten.Solution.Ragstore.WebApi.Bases;

/// <summary>
/// 
/// </summary>
[Authorize]
[Route("[Controller]")]
public class ProfileController(ILifetimeScope lifetimeScope) : BaseApiController(lifetimeScope)
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet("")]
    public Task<IActionResult> Resume()
    {
        var userEmail = User.Identity.Name;
        return Task.FromResult<IActionResult>(Ok(userEmail));
    }
}