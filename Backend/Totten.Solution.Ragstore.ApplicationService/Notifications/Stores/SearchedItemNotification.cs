namespace Totten.Solution.Ragstore.ApplicationService.Notifications.Stores;

using MediatR;

public class SearchedItemNotification : INotification
{
    public required int ItemId { get; init; }
}
