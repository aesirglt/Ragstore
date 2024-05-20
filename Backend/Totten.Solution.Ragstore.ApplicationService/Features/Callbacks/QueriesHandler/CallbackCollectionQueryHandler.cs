namespace Totten.Solution.Ragstore.ApplicationService.Features.Callbacks.QueriesHandler;

using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.ItemsAggregation.Queries;
using Totten.Solution.Ragstore.Domain.Features.CallbackAggregation;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public class CallbackCollectionQueryHandler : IRequestHandler<CallbackCollectionQuery, Result<Exception, IQueryable<Callback>>>
{
    private ICallbackRepository _repository;

    public CallbackCollectionQueryHandler(ICallbackRepository repository)
    {
        _repository = repository;
    }

    public Task<Result<Exception, IQueryable<Callback>>> Handle(CallbackCollectionQuery request, CancellationToken cancellationToken)
    {
        return Result<Exception, IQueryable<Callback>>.Ok(_repository.GetAll()).AsTask();
    }
}
