namespace Totten.Solution.Ragstore.ApplicationService.Notifications.Agents.Handlers;

using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.Domain.Features.Servers;

public class UpdateTimeNotificationHandler : INotificationHandler<UpdateTimeNotification>
{
    private IServerRepository _serverRepository;
    public UpdateTimeNotificationHandler(IServerRepository serverRepository)
    {
        _serverRepository = serverRepository;
    }
    public async Task Handle(UpdateTimeNotification notification, CancellationToken cancellationToken)
    {
        var server = _serverRepository.GetByName(notification.Server);

        await server.IfSucc(async succ =>
        {
            succ.UpdatedAt = DateTime.Now;
            await _serverRepository.Update(succ);
        });
    }
}
