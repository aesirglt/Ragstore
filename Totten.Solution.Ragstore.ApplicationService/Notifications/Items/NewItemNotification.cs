namespace Totten.Solution.Ragstore.ApplicationService.Notifications.Items;

using MediatR;

public record NewItemNotification : INotification
{
    public required string Name { get; init; }
    public required string Location { get; init; }
}
