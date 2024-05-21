namespace Totten.Solution.Ragstore.ApplicationService.Notifications.Stores;

using MediatR;
using System;
public record NewStoreNotification : INotification
{
    public required string Server { get; init; }
    public required string StoreType { get; init; }
    public required string Merchant { get; init; }
    public required string Where { get; init; }
    public required DateTime Date { get; init; }
    public required List<NewStoreNotificationItem> Items { get; init; }

    public class NewStoreNotificationItem
    {
        public int ItemId { get; set; }
        public double ItemPrice { get; set; }
    }
}
