namespace Totten.Solution.Ragstore.ApplicationService.Notifications.Stores.Handlers;
using MediatR;

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Notifications.Callbacks;
using Totten.Solution.Ragstore.Domain.Features.CallbackAggregation;

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

    public Task Handle(NewStoreNotification notify, CancellationToken cancellationToken)
    {
        try
        {
            List<int> itemsIds = notify.Items.Select(it => it.ItemId).ToList();
            var callback = _repository.GetAllByFilter(x => x.Server == notify.Server)
                                           .Where(c => itemsIds.Any(itemId => itemId == c.ItemId))
                                           .ToList();

            if (callback is { Count: > 0 })
            {
                _ = notify.Items
                          .Select(it => callback.Any(c => c.ItemId == it.ItemId && it.ItemPrice <= c.ItemPrice) ? it : null)
                          .Where(it => it != null)
                          .Select(it => new
                          {
                              notify = it,
                              callback = callback.FirstOrDefault(c => c.ItemId == it?.ItemId)
                          })
                          .Select(selected => _mediator.Publish(new CallbackNotification
                          {
                              Server = notify.Server,
                              Location = notify.Where,
                              Price = selected.notify?.ItemPrice ?? -1,
                              ItemId = selected.notify?.ItemId ?? -1,
                              Level = selected.callback?.Level ?? ECallbackType.None,
                              UserCellphone = selected.callback?.UserCellphone ?? ""
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
