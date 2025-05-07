namespace Totten.Solution.RagnaComercio.ApplicationService.Features.Callbacks.CommandsHandler;
using FunctionalConcepts;
using FunctionalConcepts.Errors;
using FunctionalConcepts.Results;
using MediatR;
using System;
using System.Threading.Tasks;
using Totten.Solution.RagnaComercio.ApplicationService.Features.Callbacks.Commands;
using Totten.Solution.RagnaComercio.Domain.Features.CallbackAggregation;

public class CallbackRemoveCommandHandler(ICallbackRepository repository)
    : IRequestHandler<CallbackRemoveCommand, Result<Success>>
{
    private readonly ICallbackRepository _repository = repository;

    public async Task<Result<Success>> Handle(CallbackRemoveCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var maybeCallback = await _repository.GetById(request.Id);

            if (maybeCallback.IsNone)
                return NotFoundError.New($"Notificação com o Id: '{request.Id}' não foi encontrada.");

            await maybeCallback.ThenAsync(async callback =>
            {
                _ = await _repository.Remove(callback);
            });

            return Result.Success;
        }
        catch (Exception ex)
        {
            UnhandledError error = ("Erro ao salvar um callback", ex);
            return error;
        }
    }
}
