namespace Totten.Solution.Ragstore.ApplicationService.Notifications.Items.Handlers;
using MediatR;
using System;
using System.Threading.Tasks;

public class NewItemNotificationHandler : INotificationHandler<NewItemNotification>
{
    public NewItemNotificationHandler()
    {
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
