namespace Totten.Solution.RagnaComercio.ApplicationService.Notifications.Callbacks.Handlers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;
using System.Threading.Tasks;
using Totten.Solution.RagnaComercio.Domain.Features.CallbackAggregation;

public class CallbackNotificationHandler : INotificationHandler<CallbackNotification>
{
    private CultureInfo _cultura;
    private ICallbackScheduleRepository _callbackScheduleRepository;
    public CallbackNotificationHandler(ICallbackScheduleRepository callbackScheduleRepository)
    {
        _callbackScheduleRepository = callbackScheduleRepository;
        _cultura = new CultureInfo("pt-BR");
    }

    public async Task Handle(CallbackNotification notify, CancellationToken cancellationToken)
    {
        try
        {
            if(notify.CallbackType == EStoreCallbackType.None)
                return;
            
            _ = await _callbackScheduleRepository.Save(new CallbackSchedule
            {
                Id = Guid.NewGuid(),
                Name = $"Contact:{notify.CallbackId}-UserCellphone-Server:{notify.Server}-ItemId:{notify.ItemId}-Price:{notify.Price}",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Contact = "notify.Contact",
                Sended = false,
                Body = @$"RagnaStore, item: *{notify.ItemId}* em *{notify.Location}* por *{notify.Price.ToString("N2", _cultura)}* servidor: {notify.Server}"
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
