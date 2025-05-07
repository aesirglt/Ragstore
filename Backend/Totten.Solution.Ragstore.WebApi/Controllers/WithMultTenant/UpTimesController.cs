namespace Totten.Solution.Ragstore.WebApi.Controllers.WithMultTenant;

using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Totten.Solution.Ragstore.ApplicationService.Notifications.Agents;
using Totten.Solution.Ragstore.WebApi.Bases;

/// <summary>
/// Endpoint responsavel por adicionar ultimo horario de atualização do servidor.
/// </summary>
/// <remarks>
/// 
/// </remarks>
/// <param name="lifetimeScope"></param>
[ApiController]
[Authorize]
public class UpTimesController(ILifetimeScope lifetimeScope) : BaseApiController(lifetimeScope)
{
    /// <summary>
    /// Cria um ponto de horario de atualização
    /// </summary>
    /// <param name="server">Servidor</param>
    /// <returns></returns>
    [HttpPost("up-times")]
    [ProducesResponseType<AcceptedResult>(statusCode: 202)]
    public async Task<IActionResult> Post(
        [FromQuery] string server)
            => await HandleEvent(new UpdateTimeNotification
            {
                Server = server,
                UpdatedAt = DateTime.UtcNow,
            });
}
