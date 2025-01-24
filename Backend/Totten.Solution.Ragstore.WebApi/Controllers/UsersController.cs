namespace Totten.Solution.Ragstore.WebApi.Controllers;

using Autofac;
using FunctionalConcepts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Totten.Solution.Ragstore.Infra.Data.Contexts.StoreServerContext;
using Totten.Solution.Ragstore.WebApi.Bases;
using Totten.Solution.Ragstore.WebApi.SystemConstants;

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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="server"></param>
    /// <returns></returns>
    [HttpPost("{server}/migrate")]
    [ProducesResponseType<Success>(statusCode: 201)]
    public async Task<IActionResult> Create([FromRoute]string server)
    {
        var opt = new DbContextOptionsBuilder<ServerStoreContext>().UseNpgsql(SysConstantDBConfig.DEFAULT_CONNECTION_STRING.Replace("{dbName}", server));
        var ctx = new ServerStoreContext(opt.Options);
        await ctx.Database.MigrateAsync();
        return Ok();
    }
}
