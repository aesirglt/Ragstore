namespace Totten.Solution.Ragstore.ApplicationService.Features.Callbacks.CommandsHandler;

using AutoMapper;
using FunctionalConcepts;
using FunctionalConcepts.Errors;
using FunctionalConcepts.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.Callbacks.Commands;
using Totten.Solution.Ragstore.Domain.Features.CallbackAggregation;

public class CallbackSaveCommandHandler(IMapper mapper, ICallbackRepository repository)
    : IRequestHandler<CallbackSaveCommand, Result<Success>>
{
    private readonly IMapper _mapper = mapper;
    private readonly ICallbackRepository _repository = repository;

    public async Task<Result<Success>> Handle(CallbackSaveCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var callback = _mapper.Map<Callback>(request);

            _ = await _repository.Save(callback);

            return Result.Success;
        }
        catch (Exception ex)
        {
            UnhandledError error = ("Erro ao salvar um callback", ex);
            return error;
        }
    }
}
