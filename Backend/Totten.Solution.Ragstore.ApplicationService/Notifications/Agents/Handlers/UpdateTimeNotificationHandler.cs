namespace Totten.Solution.Ragstore.ApplicationService.Notifications.Agents.Handlers;

using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.Domain.Features.Agents;

public class UpdateTimeNotificationHandler : INotificationHandler<UpdateTimeNotification>
{
    private IMediator _mediator;
    private IUpdateTimeRepository _repository;

    public UpdateTimeNotificationHandler(IServiceProvider provider)
    {
        var scoped = provider.CreateScope();
        _mediator = scoped.ServiceProvider.GetService<IMediator>() ?? throw new Exception();
        _repository = scoped.ServiceProvider.GetService<IUpdateTimeRepository>() ?? throw new Exception();
    }
    public async Task Handle(UpdateTimeNotification notification, CancellationToken cancellationToken)
    {
        var updateTime = _repository.GetAllByFilter(upd => notification.Server.Equals(upd.Name)).FirstOrDefault();

        if (updateTime is null)
            return;

        updateTime.UpdatedAt = notification.UpdatedAt;

        _ = await _repository.Update(updateTime);
    }
}
