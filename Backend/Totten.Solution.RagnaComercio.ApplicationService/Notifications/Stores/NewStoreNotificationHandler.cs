namespace Totten.Solution.RagnaComercio.ApplicationService.Notifications.Stores;
using MediatR;

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Totten.Solution.RagnaComercio.ApplicationService.Notifications.Callbacks;
using Totten.Solution.RagnaComercio.Domain.Features.CallbackAggregation;

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
            var notifyCallbackType = Enum.Parse<EStoreCallbackType>(notify.StoreType);

            List<int> itemsIds = [.. notify.Items.Select(it => it.ItemId)];

            var callbacks = _repository.GetAll(x => x.Server!.Name == notify.Server && x.StoreType == notifyCallbackType)
                                       .Where(c => itemsIds.Any(itemId => itemId == c.ItemId))
                                       .ToList();

            if (callbacks is { Count: > 0 })
            {
                var tasks = notify.Items
                          .Where(it => callbacks.Any(c => c.ItemId == it.ItemId && it.ItemPrice <= c.ItemPrice))
                          .Select(it => new
                          {
                              notify = it,
                              callbacks = callbacks.Where(c => c.ItemId == it?.ItemId)
                          })
                          .SelectMany(selected => selected.callbacks.Select(cb =>
                               _mediator.Publish(new CallbackNotification
                               {
                                   CallbackId = cb.Id,
                                   UserId = cb.UserId,
                                   StoreId = notify.StoreId,
                                   Server = notify.Server,
                                   Location = notify.Where,
                                   CallbackType = cb?.StoreType ?? EStoreCallbackType.None,
                                   Price = selected?.notify?.ItemPrice ?? -1,
                                   ItemId = selected?.notify?.ItemId ?? -1,
                                   CharacterName = notify.Merchant,
                                   StoreName = notify.StoreName,
                               })
                            ))
                          .ToArray();

                Task.WaitAll(tasks, cancellationToken);
            }
            ;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        return Task.CompletedTask;
    }
}
