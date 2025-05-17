namespace Totten.Solution.RagnaComercio.ApplicationService.Notifications.Callbacks;

using MediatR;
using Totten.Solution.RagnaComercio.Domain.Features.CallbackAggregation;

public record CallbackNotification : INotification
{
    public required Guid CallbackId { get; init; }
    public required Guid UserId { get; init; }
    public required int StoreId { get; init; }
    public required string Server { get; init; }
    public required int ItemId { get; init; }
    public required double Price { get; init; }
    public required string Location { get; init; }
    public required string StoreName { get; init; }
    public required string CharacterName { get; init; }
    public required EStoreCallbackType CallbackType { get; init; }
}
