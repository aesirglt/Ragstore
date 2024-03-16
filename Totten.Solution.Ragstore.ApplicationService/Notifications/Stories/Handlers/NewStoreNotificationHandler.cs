namespace Totten.Solution.Ragstore.ApplicationService.Notifications.Stories.Handlers;
using MediatR;

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Notifications.Items;
using Totten.Solution.Ragstore.Domain.Features.Items;

public class NewStoreNotificationHandler : INotificationHandler<NewStoreNotification>
{
    private IMediator _mediator;
    private IItemRepository _itemRepository;

    public NewStoreNotificationHandler(IServiceProvider provider)
    {
        var scoped = provider.CreateScope();
        _mediator = scoped.ServiceProvider.GetService<IMediator>() ?? throw new Exception();
        _itemRepository = scoped.ServiceProvider.GetService<IItemRepository>() ?? throw new Exception();
    }

    public async Task Handle(NewStoreNotification notification, CancellationToken cancellationToken)
    {
        try
        {
            foreach (var item in notification.Items)
            {
                await _itemRepository.Save(new Item
                {
                    Name = item.Key,
                    Mercant = notification.Mercant,
                    Price = item.Value,
                    Date = notification.Date
                });

                _ = _mediator.Publish(new NewItemNotification
                {
                    Location = notification.Location,
                    Name = item.Key
                });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
