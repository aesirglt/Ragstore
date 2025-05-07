namespace Totten.Solution.RagnaComercio.ApplicationService.Notifications.ODataFilters;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.RagnaComercio.Domain.Features.ItemsAggregation;
using Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Vendings;

public class ODataFilterNotificationHandler(ILogger<ODataFilterNotificationHandler> logger, IServiceProvider provider)
    : INotificationHandler<ODataFilterNotification>
{
    private readonly IServiceScope _serviceScope = provider.CreateScope();

    public async Task Handle(ODataFilterNotification notification, CancellationToken cancellationToken)
    {
        try
        {
            var searchedItemRepository = _serviceScope.ServiceProvider.GetRequiredService<ISearchedItemRepository>();
            var storeItemRepository = _serviceScope.ServiceProvider.GetRequiredService<IVendingStoreItemRepository>();

            var updatedAt = DateTime.Parse(DateTime.UtcNow.ToString("yyyy-MM-01T00:00:00Z"), CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);

            var storeItem =
                storeItemRepository.GetAll(x => x.ItemId == notification.ItemId && x.UpdatedAt >= updatedAt)
                .ToList();

            if (storeItem.Count > 0)
            {
                await searchedItemRepository.Save(new SearchedItem
                {
                    Id = 0,
                    ItemId = notification.ItemId,
                    CreatedAt = DateTime.UtcNow,
                    Name = storeItem.First().Name,
                    Quantity = storeItem.Sum(si => si.Quantity),
                    Average = (long)( storeItem.Sum(si => si.Price) / storeItem.Count ),
                });
            }
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, "Erro on Handler: {HandlerName}, Message: {Message}", nameof(ODataFilterNotificationHandler), ex.Message);
        }
    }
}
