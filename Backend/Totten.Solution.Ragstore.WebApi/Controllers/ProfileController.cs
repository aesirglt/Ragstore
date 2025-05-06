namespace Totten.Solution.Ragstore.WebApi.Controllers;

using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Totten.Solution.Ragstore.WebApi.Bases;
using System.Linq;

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
        var user = HttpContext.User;
        var email = user.Identity.Name;
        var name = user.Claims.FirstOrDefault(c => c.Type == "name")?.Value ?? email;
        var id = user.Claims.FirstOrDefault(c => c.Type == "sub" || c.Type == "id")?.Value ?? email;

        return Task.FromResult<IActionResult>(Ok(new {
            id,
            name,
            email
        }));
    }
}