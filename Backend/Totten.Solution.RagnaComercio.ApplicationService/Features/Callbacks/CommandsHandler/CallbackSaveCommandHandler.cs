namespace Totten.Solution.RagnaComercio.ApplicationService.Features.Callbacks.CommandsHandler;

using AutoMapper;
using FunctionalConcepts;
using FunctionalConcepts.Errors;
using FunctionalConcepts.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.RagnaComercio.ApplicationService.Features.Callbacks.Commands;
using Totten.Solution.RagnaComercio.Domain.Features.CallbackAggregation;

public class CallbackSaveCommandHandler(IMapper mapper, ICallbackRepository repository)
    : IRequestHandler<CallbackSaveCommand, Result<Success>>
{
    private readonly IMapper _mapper = mapper;
    private readonly ICallbackRepository _repository = repository;

    public async Task<Result<Success>> Handle(CallbackSaveCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _ = await _repository.Save(new Callback
            {
                Id = Guid.NewGuid(),
                ItemId = request.ItemId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                ItemPrice = request.ItemPrice,
                Name = $"Callback for {request.ItemId}",
                UserId = request.UserId,
                ServerId = request.ServerId,
                StoreType = EStoreCallbackType.VendingStore,
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
