namespace Totten.Solution.Ragstore.ApplicationService.Notifications.Items;

using MediatR;

public record NewItemNotification : INotification
{
    public required string Server { get; init; }
    public required string Name { get; init; }
    public required double Price { get; init; }
    public required string Location { get; init; }
}
