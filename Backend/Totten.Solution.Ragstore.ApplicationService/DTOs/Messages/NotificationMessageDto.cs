namespace Totten.Solution.Ragstore.ApplicationService.DTOs.Messages;

using Totten.Solution.Ragstore.ApplicationService.Interfaces;

public record NotificationMessageDto : ISendable<NotificationMessageDto>
{
    public required string To { get; init; }
    public required string From { get; init; }
    public required string Content { get; init; }
}
