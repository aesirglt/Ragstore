namespace Totten.Solution.Ragstore.ApplicationService.Notifications.Callbacks;

using MediatR;
using Totten.Solution.Ragstore.Domain.Features.CallbackAggregation;

public record CallbackNotification : INotification
{
    public required string Server { get; init; }
    public required int ItemId { get; init; }
    public required double Price { get; init; }
    public required string Location { get; init; }
    public required ECallbackType Level { get; init; }
    public required string UserCellphone { get; init; }
}
