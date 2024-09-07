namespace Totten.Solution.Ragstore.ApplicationService.Features.Callbacks.QueriesHandler;

using FunctionalConcepts.Results;using FunctionalConcepts;
using MediatR;
using System;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.ItemsAggregation.Queries;
using Totten.Solution.Ragstore.Domain.Features.CallbackAggregation;

public class CallbackCollectionByUserIdQueryHandler : IRequestHandler<CallbackCollectionByUserIdQuery, Result<IQueryable<Callback>>>
{
    private ICallbackRepository _repository;

    public CallbackCollectionByUserIdQueryHandler(ICallbackRepository repository)
    {
        _repository = repository;
    }

    public Task<Result<IQueryable<Callback>>> Handle(CallbackCollectionByUserIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
