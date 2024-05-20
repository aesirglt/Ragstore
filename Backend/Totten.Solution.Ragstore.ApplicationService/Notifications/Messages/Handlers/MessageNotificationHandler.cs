namespace Totten.Solution.Ragstore.ApplicationService.Notifications.Messages.Handlers;
using MediatR;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.DTOs.Messages;
using Totten.Solution.Ragstore.ApplicationService.Interfaces;

public class MessageNotificationHandler : INotificationHandler<MessageNotification>
{
    private IMessageService<NotificationMessageDto> _messageService;

    public MessageNotificationHandler(IMessageService<NotificationMessageDto> service)
    {
        _messageService = service;
    }

    public async Task Handle(MessageNotification notification, CancellationToken cancellationToken)
    {
        _ = await _messageService.Send(new NotificationMessageDto
        {
            To = notification.Contact,
            Content = notification.Body,
            From = "RagnaStore - Seu mercado de ragnarok online"
        });
    }
}
