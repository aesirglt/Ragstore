namespace Totten.Solution.Ragstore.ApplicationService.Notifications.Agents.Handlers;

using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.Domain.Features.Servers;
using Totten.Solution.Ragstore.Infra.Cross.Statics;

public class UpdateTimeNotificationHandler(IServerRepository serverRepository) : INotificationHandler<UpdateTimeNotification>
{
    private readonly IServerRepository _serverRepository = serverRepository;

    public async Task Handle(UpdateTimeNotification notification, CancellationToken cancellationToken)
    {
        var server = await _serverRepository.GetByName(notification.Server).AsTask();

        await server.ThenAsync(async succ =>
        {
            succ.UpdatedAt = DateTime.Now;
            await _serverRepository.Update(succ);
        });
    }
}
