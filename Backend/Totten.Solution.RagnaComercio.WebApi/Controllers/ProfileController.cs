namespace Totten.Solution.RagnaComercio.WebApi.Controllers;

using Autofac;
using Microsoft.AspNetCore.Mvc;
using Totten.Solution.RagnaComercio.ApplicationService.Features.Users.Queries;
using Totten.Solution.RagnaComercio.ApplicationService.ViewModels.Users;
using Totten.Solution.RagnaComercio.Domain.Features.Users;
using Totten.Solution.RagnaComercio.WebApi.Bases;
using Totten.Solution.RagnaComercio.WebApi.Filters;

/// <summary>
/// 
/// </summary>
[Route("[Controller]")]
public class ProfileController(ILifetimeScope lifetimeScope) : BaseApiController(lifetimeScope)
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [CustomAuthorizeAttributte()]
    public async Task<IActionResult> Resume()
        => string.IsNullOrWhiteSpace(base.UserNormalizedEmail)
        ? Ok(null)
        : await base.HandleQuery<User, UserDetailViewModel>(new UserByEmailQuery
        {
            NormalizedEmail = base.UserNormalizedEmail
        });
}