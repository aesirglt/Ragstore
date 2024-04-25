namespace Totten.Solution.Ragstore.ApplicationService.Features.Callbacks.QueriesHandler;

using MediatR;
using System;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.ItemsAggregation.Queries;
using Totten.Solution.Ragstore.Domain.Features.Callbacks;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public class CallbackCollectionByUserIdQueryHandler : IRequestHandler<CallbackCollectionByUserIdQuery, Result<Exception, IQueryable<Callback>>>
{
    private ICallbackRepository _repository;

    public CallbackCollectionByUserIdQueryHandler(ICallbackRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Exception, IQueryable<Callback>>> Handle(CallbackCollectionByUserIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
