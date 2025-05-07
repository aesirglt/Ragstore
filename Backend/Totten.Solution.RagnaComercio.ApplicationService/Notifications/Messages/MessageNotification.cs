namespace Totten.Solution.RagnaComercio.ApplicationService.Notifications.Messages;

using MediatR;

public record MessageNotification : INotification
{
    public required string Contact { get; init; }
    public required string Body { get; init; }
}
