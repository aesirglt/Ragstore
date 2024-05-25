namespace Totten.Solution.Ragstore.ApplicationService.Features.Callbacks.QueriesHandler;

using LanguageExt;
using LanguageExt.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.ItemsAggregation.Queries;
using Totten.Solution.Ragstore.Domain.Features.CallbackAggregation;

public class CallbackCollectionQueryHandler : IRequestHandler<CallbackCollectionQuery, Result<IQueryable<Callback>>>
{
    private ICallbackRepository _repository;

    public CallbackCollectionQueryHandler(ICallbackRepository repository)
    {
        _repository = repository;
    }

    public Task<Result<IQueryable<Callback>>> Handle(CallbackCollectionQuery request, CancellationToken cancellationToken)
    {
        return new Result<IQueryable<Callback>>(_repository.GetAll()).AsTask();
    }
}
