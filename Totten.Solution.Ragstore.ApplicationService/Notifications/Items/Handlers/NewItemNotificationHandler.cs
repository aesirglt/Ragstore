namespace Totten.Solution.Ragstore.ApplicationService.Notifications.Items.Handlers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

public class NewItemNotificationHandler : INotificationHandler<NewItemNotification>
{
    private IMediator _mediator;
    public NewItemNotificationHandler(IServiceProvider provider)
    {
        var scoped = provider.CreateScope();
        _mediator = scoped.ServiceProvider.GetService<IMediator>() ?? throw new Exception();
    }

    public async Task Handle(NewItemNotification notification, CancellationToken cancellationToken)
    {
        try
        {
            //verificar usuarios que querem notificação desse item
            //verificar tipo de notificacao
            //enviar notificacao
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
