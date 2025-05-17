namespace Totten.Solution.RagnaComercio.ApplicationService.Notifications.Callbacks;
using MediatR;
using System;
using System.Globalization;
using System.Threading.Tasks;
using Totten.Solution.RagnaComercio.Domain.Features.CallbackAggregation;
using Totten.Solution.RagnaComercio.Domain.Features.Users;

public class CallbackNotificationHandler : INotificationHandler<CallbackNotification>
{
    private CultureInfo _cultura;
    private ICallbackScheduleRepository _callbackScheduleRepository;
    private IUserRepository _userRepository;
    public CallbackNotificationHandler(ICallbackScheduleRepository callbackScheduleRepository, IUserRepository userRepository)
    {
        _cultura = new CultureInfo("pt-BR");
        _callbackScheduleRepository = callbackScheduleRepository;
        _userRepository = userRepository;
    }

    public async Task Handle(CallbackNotification notify, CancellationToken cancellationToken)
    {
        try
        {
            var name = $"{notify.CallbackId}#{notify.Server}#{notify.StoreId}#{notify.StoreName}#{notify.CharacterName}#{notify.ItemId}#{notify.Price}#{notify.Location}";

            var existSchedule = _callbackScheduleRepository.GetAll(x => x.Name.Equals(name)).Any();

            if (notify.CallbackType == EStoreCallbackType.None || existSchedule)
                return;

            var maybeUser = await _userRepository.GetById(notify.UserId);

            await maybeUser.ThenAsync(async user =>
            {
                var isDiscord = string.IsNullOrWhiteSpace(user.DiscordUser);

                _ = await _callbackScheduleRepository.Save(new CallbackSchedule
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    CallbackId = notify.CallbackId,
                    Destination = isDiscord ? DestinationType.Discord : DestinationType.Whatsapp,
                    Contact = isDiscord ? user.DiscordUser : user.PhoneNumber,
                    Sended = false,
                    Body = @$"RagnaComercio, item: *{notify.ItemId}* em *{notify.Location}* por *{notify.Price.ToString("N2", _cultura)}* servidor: {notify.Server}"
                });
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
