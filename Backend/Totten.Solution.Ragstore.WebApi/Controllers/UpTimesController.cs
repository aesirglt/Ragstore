namespace Totten.Solution.Ragstore.WebApi.Controllers;

using Autofac;
using Microsoft.AspNetCore.Mvc;
using Totten.Solution.Ragstore.ApplicationService.Notifications.Agents;
using Totten.Solution.Ragstore.WebApi.Bases;

/// <summary>
/// 
/// </summary>
[ApiController]
public class UpTimesController : BaseApiController
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="lifetimeScope"></param>
    public UpTimesController(ILifetimeScope lifetimeScope) : base(lifetimeScope)
    {
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="server"></param>
    /// <returns></returns>
    [HttpPost("up-times")]
    public async Task<IActionResult> Post(
        [FromQuery] string server)
            => await HandleEvent(new UpdateTimeNotification
            {
                Server = server,
                UpdatedAt = DateTime.Now
            }, server);
}
