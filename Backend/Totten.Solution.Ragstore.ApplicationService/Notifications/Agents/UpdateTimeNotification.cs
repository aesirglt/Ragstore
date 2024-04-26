namespace Totten.Solution.Ragstore.ApplicationService.Notifications.Agents;

using Autofac;
using MediatR;

public class UpdateTimeNotification : INotification
{
    public string Server { get; set; } = string.Empty;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public required ILifetimeScope Scope { get; set; }
}
