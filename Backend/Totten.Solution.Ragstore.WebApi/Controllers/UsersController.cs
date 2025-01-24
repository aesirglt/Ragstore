namespace Totten.Solution.Ragstore.WebApi.Controllers;

using Autofac;
using Microsoft.AspNetCore.Mvc;
using Totten.Solution.Ragstore.WebApi.Bases;

/// <summary>
/// Endpoint responsavel por usuários
/// </summary>
[ApiController]
[Route("[controller]")]
public class UsersController : BaseApiController
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="lifetimeScope"></param>
    public UsersController(ILifetimeScope lifetimeScope) : base(lifetimeScope)
    {
    }
}
