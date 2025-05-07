namespace Totten.Solution.RagnaComercio.ApplicationService.DTOs.Messages;

using Totten.Solution.RagnaComercio.ApplicationService.Interfaces;

public record NotificationMessageDto : ISendable<NotificationMessageDto>
{
    public required string To { get; init; }
    public required string From { get; init; }
    public required string Content { get; init; }
}
