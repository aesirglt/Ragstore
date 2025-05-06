namespace Totten.Solution.Ragstore.ApplicationService.Features.Callbacks.QueriesHandler;

using FunctionalConcepts.Results;
using MediatR;
using System;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.ItemsAggregation.Queries;
using Totten.Solution.Ragstore.Domain.Features.CallbackAggregation;
using Totten.Solution.Ragstore.Infra.Cross.Statics;

public class CallbackCollectionByUserIdQueryHandler : IRequestHandler<CallbackCollectionByUserIdQuery, Result<IQueryable<Callback>>>
{
    private ICallbackRepository _repository;

    public CallbackCollectionByUserIdQueryHandler(ICallbackRepository repository)
    {
        _repository = repository;
    }

    public Task<Result<IQueryable<Callback>>> Handle(CallbackCollectionByUserIdQuery request, CancellationToken cancellationToken)
    {
        var callbacks = _repository.GetAll().Where(c => c.CallbackOwnerId == request.UserId);
        return Result.Of(callbacks).AsTask();
    }
}
