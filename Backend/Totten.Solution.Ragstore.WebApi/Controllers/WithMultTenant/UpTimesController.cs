namespace Totten.Solution.Ragstore.WebApi.Controllers.WithMultTenant;

using Autofac;
using Microsoft.AspNetCore.Mvc;
using Totten.Solution.Ragstore.ApplicationService.Notifications.Agents;
using Totten.Solution.Ragstore.WebApi.Bases;

/// <summary>
/// Endpoint responsavel por adicionar ultimo horario de atualização do servidor.
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
    /// Cria um ponto de horario de atualização
    /// </summary>
    /// <param name="server">Servidor</param>
    /// <returns></returns>
    [HttpPost("up-times")]
    public async Task<IActionResult> Post(
        [FromQuery] string server)
            => await HandleEvent(new UpdateTimeNotification
            {
                Server = server,
                UpdatedAt = DateTime.Now,
            });
}
