namespace Totten.Solution.Ragstore.WebApi.Controllers;

using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Totten.Solution.Ragstore.ApplicationService.Features.Users.Queries;
using Totten.Solution.Ragstore.ApplicationService.ViewModels.Users;
using Totten.Solution.Ragstore.Domain.Features.Users;
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
    [HttpGet]
    public async Task<IActionResult> Resume()
        => await base.HandleQuery<User, UserDetailViewModel>(new UserByEmailQuery
        {
            NormalizedEmail = base.UserNormalizedEmail
        });
}