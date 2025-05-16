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

public class CallbackMessageHandler(
    ILifetimeScope scope)
    : IRequestHandler<CallbackMessageCommand, Result<Success>>
{
    public async Task<Result<Success>> Handle(CallbackMessageCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var _repository = scope.Resolve<ICallbackScheduleRepository>();
            var _messageService = scope.Resolve<IMessageService<DiscordMessageDto>>();

            var scheduledCallbacks = _repository.GetAll(x => !x.Sended).ToList();

            foreach (var callback in scheduledCallbacks)
            {
                var result = await _messageService.Send(new DiscordMessageDto
                {
                    UserName = callback.Name,
                    Content = callback.Body
                });

                await result.ThenAsync(async _ =>
                {
                    await _repository.Update(callback with
                    {
                        UpdatedAt = DateTime.UtcNow,
                        Sended = true
                    });
                });
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
