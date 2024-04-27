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

    public async Task Handle(NewItemNotification notify, CancellationToken cancellationToken)
    {
        try
        {
            if (notify.Level is ECallbackType.AGENT or ECallbackType.SYSTEM)
            {
                _ = _mediator.Publish(new MessageNotification
                {
                    Contact = notify.UserCellphone,
                    Body = @$"RagnaStore, item: *{notify.ItemId}* em *{notify.Location}* por *{notify.Price}* servidor: {notify.Server} as {DateTime.Now.ToString("HH:mm:ss")}"
                });
                return;
            }

            //adiciona na fila rabiitmq para proximo envios de callback
            //envio de callbacks para pessoas comuns serão apenas a cada meia hora ou 15 minutos, a depender do sistema.
            //ainda não defini essa parte
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
