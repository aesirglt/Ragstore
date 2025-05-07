namespace Totten.Solution.RagnaComercio.ApplicationService.Features.Callbacks.QueriesHandler;

using FunctionalConcepts.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.RagnaComercio.ApplicationService.Features.ItemsAggregation.Queries;
using Totten.Solution.RagnaComercio.Domain.Features.CallbackAggregation;

public class CallbackCollectionQueryHandler(ICallbackRepository repository) : IRequestHandler<CallbackCollectionQuery, Result<IQueryable<Callback>>>
{
    private readonly ICallbackRepository _repository = repository;

    public Task<Result<IQueryable<Callback>>> Handle(CallbackCollectionQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(Result.Of(_repository.GetAll()));
    }
}
