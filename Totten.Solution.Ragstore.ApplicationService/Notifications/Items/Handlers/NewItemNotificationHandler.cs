namespace Totten.Solution.Ragstore.ApplicationService.Notifications.Items.Handlers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Notifications.Messages;
using Totten.Solution.Ragstore.Domain.Features.Callbacks;

public class NewItemNotificationHandler : INotificationHandler<NewItemNotification>
{
    private IMediator _mediator;
    private ICallbackRepository _callbackRepository;
    public NewItemNotificationHandler(IServiceProvider provider)
    {
        var scoped = provider.CreateScope();
        _mediator = scoped.ServiceProvider.GetService<IMediator>() ?? throw new Exception();
        _callbackRepository = scoped.ServiceProvider.GetService<ICallbackRepository>() ?? throw new Exception();
    }

    public async Task Handle(NewItemNotification notification, CancellationToken cancellationToken)
    {
        try
        {
            //verificar usuarios que querem notificação desse item
            var callbacks = await _callbackRepository.GetByItemAndPriceAll(notification.Name, notification.Price);

            if (callbacks is { Count: > 0 })
            {
                callbacks.ForEach(cb =>
                {
                    if (cb.Level is ECallbackType.AGENT or ECallbackType.SYSTEM)
                    {
                        _mediator.Publish(new MessageNotification
                        {
                            Contact = cb.UserCellphone,
                            Body = @$"RagnaStore, item: *{notification.Name}* em *{notification.Location}* por *{notification.Price}* as {DateTime.Now.ToString("HH:mm:ss")}"
                        });
                        return;
                    }

                    //adiciona na fila rabiitmq para proximo envios de callback
                    //envio de callbacks para pessoas comuns serão apenas a cada meia hora ou 15 minutos, a depender do sistema.
                    //ainda não defini essa parte
                });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
