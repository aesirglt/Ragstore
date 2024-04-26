namespace Totten.Solution.Ragstore.ApplicationService.Notifications.Agents.Handlers;

using Autofac;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.Domain.Features.Agents;

public class UpdateTimeNotificationHandler : INotificationHandler<UpdateTimeNotification>
{
    public async Task Handle(UpdateTimeNotification notification, CancellationToken cancellationToken)
    {
        var scoped = notification.Scope;
        var repository = scoped.Resolve<IUpdateTimeRepository>() ?? throw new Exception();
        var updateTime = repository.GetAllByFilter(upd => notification.Server.Equals(upd.Name)).FirstOrDefault();

        if (updateTime is null)
            return;

        updateTime.UpdatedAt = notification.UpdatedAt;

        _ = await repository.Update(updateTime);
    }
}
