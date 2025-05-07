namespace Totten.Solution.RagnaComercio.WebApi.Controllers;

using FunctionalConcepts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Totten.Solution.RagnaComercio.Infra.Data.Contexts.StoreServerContext;
using Totten.Solution.RagnaComercio.WebApi.SystemConstants;

/// <summary>
/// Endpoint responsavel por usuários
/// </summary>
[ApiController]
public class MigrateController : ControllerBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="server"></param>
    /// <param name="cod"></param>
    /// <returns></returns>
    [HttpPost("{server}/migrate")]
    [ProducesResponseType<Success>(statusCode: 201)]
    [Authorize]
    public async Task<IActionResult> Create([FromRoute] string server, [FromQuery] string cod)
    {
        if (cod != "supercode") return await Task.Run(base.Unauthorized);

        var opt = new DbContextOptionsBuilder<ServerStoreContext>().UseNpgsql(SysConstantDBConfig.DEFAULT_CONNECTION_STRING.Replace("{dbName}", server));
        var ctx = new ServerStoreContext(opt.Options);
        _ = Task.Run(ctx.Database.Migrate);
        return await Task.FromResult(Ok());
    }
}
