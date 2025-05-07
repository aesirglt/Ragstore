namespace Totten.Solution.RagnaComercio.ApplicationService.Notifications.Agents;

using MediatR;

public class UpdateTimeNotification : INotification
{
    public string Server { get; set; } = string.Empty;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
