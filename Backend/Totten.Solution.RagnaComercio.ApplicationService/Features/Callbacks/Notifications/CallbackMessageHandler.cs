namespace Totten.Solution.RagnaComercio.ApplicationService.Features.Callbacks.Notifications;

using Autofac;
using FunctionalConcepts;
using FunctionalConcepts.Errors;
using FunctionalConcepts.Results;
using MediatR;
using System;
using System.Threading.Tasks;
using Totten.Solution.RagnaComercio.ApplicationService.DTOs.Messages;
using Totten.Solution.RagnaComercio.ApplicationService.Interfaces;
using Totten.Solution.RagnaComercio.Domain.Features.CallbackAggregation;
using Totten.Solution.RagnaComercio.Domain.Features.Users;

public class CallbackMessageHandler(
    ILifetimeScope scope)
    : IRequestHandler<CallbackMessageCommand, Result<Success>>
{
    public async Task<Result<Success>> Handle(CallbackMessageCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var userRepository = scope.Resolve<IUserRepository>();
            var repositorySchedule = scope.Resolve<ICallbackScheduleRepository>();
            var messageService = scope.Resolve<IMessageService<DiscordMessageDto>>();

            var scheduledCallbacks = repositorySchedule.GetAll(x => !x.Sended && x.Destination == DestinationType.Discord).ToList();

            foreach (var callbackSchedule in scheduledCallbacks)
            {
                var result = await messageService.Send(new DiscordMessageDto
                {
                    UserName = callbackSchedule.Contact,
                    Content = callbackSchedule.Body
                });

                await result.ThenAsync(async _ =>
                    await repositorySchedule.Update(callbackSchedule with
                    {
                        UpdatedAt = DateTime.UtcNow,
                        Sended = true
                    }));
            }
            return Result.Success;
        }
        catch (Exception ex)
        {
            UnhandledError error = ("Erro ao salvar um callback", ex);
            return error;
        }
    }
}
