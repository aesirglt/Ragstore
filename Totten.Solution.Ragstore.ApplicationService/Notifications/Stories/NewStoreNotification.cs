namespace Totten.Solution.Ragstore.ApplicationService.Notifications.Stories;

using MediatR;
using System;

public record NewStoreNotification : INotification
{
    public required string Server { get; init; }
    public required string Merchant { get; init; }
    public required string Location { get; init; }
    public required DateTime Date { get; init; }
    public required Dictionary<string, double> Items { get; init; }
}
