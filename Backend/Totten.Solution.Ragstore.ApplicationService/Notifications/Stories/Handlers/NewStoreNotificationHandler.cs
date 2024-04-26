namespace Totten.Solution.Ragstore.ApplicationService.Notifications.Stories.Handlers;
using MediatR;

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Notifications.Items;
using Totten.Solution.Ragstore.Domain.Features.Callbacks;

public class NewStoreNotificationHandler : INotificationHandler<NewStoreNotification>
{
    private IMediator _mediator;
    private ICallbackRepository _repository;

    public NewStoreNotificationHandler(IServiceProvider provider)
    {
        var scoped = provider.CreateScope();
        _mediator = scoped.ServiceProvider.GetService<IMediator>() ?? throw new Exception();
        _repository = scoped.ServiceProvider.GetService<ICallbackRepository>() ?? throw new Exception();
    }

    public Task Handle(NewStoreNotification notification, CancellationToken cancellationToken)
    {
        try
        {
            var callbacks = _repository.GetAllByFilter(x => x.Server == notification.Server)
                            .AsEnumerable()
                            .Where(c => c.Items.Keys.Intersect(notification.Items.Keys).Any())
                            .ToList();

            if (callbacks is { Count: > 0 })
            {
                var itemsToCallback = notification.Items.Where(item => callbacks.Any(call => call.Items.Keys.Contains(item.Key)));

                _ = itemsToCallback.Select(item => _mediator.Publish(new NewItemNotification
                {
                    Server = notification.Server,
                    Location = notification.Where,
                    Price = item.Value,
                    ItemId = item.Key
                })).ToArray();
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        return Task.CompletedTask;
    }
}
