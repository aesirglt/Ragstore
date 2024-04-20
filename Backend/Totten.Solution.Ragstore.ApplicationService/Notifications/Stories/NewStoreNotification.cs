namespace Totten.Solution.Ragstore.ApplicationService.Notifications.Stories;

using MediatR;
using System;
using ItemId = int;
using ItemValue = double;
public record NewStoreNotification : INotification
{
    public required string Server { get; init; }
    public required string Merchant { get; init; }
    public required string Where { get; init; }
    public required DateTime Date { get; init; }
    public required Dictionary<ItemId, ItemValue> Items { get; init; }
}
