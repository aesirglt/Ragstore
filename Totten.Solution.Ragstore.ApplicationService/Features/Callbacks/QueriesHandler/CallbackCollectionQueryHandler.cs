namespace Totten.Solution.Ragstore.ApplicationService.Features.Callbacks.QueriesHandler;

using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.ItemsAggregation.Queries;
using Totten.Solution.Ragstore.Domain.Features.Callbacks;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public class CallbackCollectionQueryHandler : IRequestHandler<CallbackCollectionQuery, Result<Exception, List<Callback>>>
{
    private ICallbackRepository _repository;

    public CallbackCollectionQueryHandler(ICallbackRepository repository)
    {
        _repository = repository;
    }

    public Task<Result<Exception, List<Callback>>> Handle(CallbackCollectionQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
