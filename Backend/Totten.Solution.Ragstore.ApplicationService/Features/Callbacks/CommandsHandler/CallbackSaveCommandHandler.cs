namespace Totten.Solution.Ragstore.ApplicationService.Features.Callbacks.CommandsHandler;

using AutoMapper;
using LanguageExt.Common;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.Callbacks.Commands;
using Totten.Solution.Ragstore.Domain.Features.CallbackAggregation;
using Totten.Solution.Ragstore.Infra.Cross.Errors.EspecifiedErrors;
using Unit = LanguageExt.Unit;

public class CallbackSaveCommandHandler : IRequestHandler<CallbackSaveCommand, Result<Unit>>
{
    private IMediator _mediator;
    private IMapper _mapper;
    private ICallbackRepository _repository;

    public CallbackSaveCommandHandler(IMediator mediator, IMapper mapper, ICallbackRepository repository)
    {
        _mediator = mediator;
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<Result<Unit>> Handle(CallbackSaveCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var callback = _mapper.Map<Callback>(request);

            _ = await _repository.Save(callback);

            return new Unit();
        }
        catch (Exception ex)
        {
            return new Result<Unit>(new InternalError("Erro ao salvar um callback", ex));
        }
    }
}
