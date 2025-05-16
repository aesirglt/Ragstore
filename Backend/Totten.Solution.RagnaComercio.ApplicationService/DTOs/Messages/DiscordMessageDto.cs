namespace Totten.Solution.RagnaComercio.ApplicationService.DTOs.Messages;
using Totten.Solution.RagnaComercio.ApplicationService.Interfaces;

public class DiscordMessageDto : ISendable<DiscordMessageDto>
{
    public required string UserName { get; init; }
    public required string Content { get; init; }
}
