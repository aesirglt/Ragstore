namespace Totten.Solution.Ragstore.ApplicationService.Features.Callbacks.QueriesHandler;

using FunctionalConcepts.Results;
using MediatR;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.ItemsAggregation.Queries;
using Totten.Solution.Ragstore.ApplicationService.ViewModels.Callbacks;
using Totten.Solution.Ragstore.Domain.Features.CallbackAggregation;
using Totten.Solution.Ragstore.Infra.Cross.Statics;

public class CallbackCollectionByUserIdQueryHandler(ICallbackRepository repository)
    : IRequestHandler<CallbackCollectionByUserIdQuery, Result<IQueryable<CallbackResumeViewModel>>>
{
    private ICallbackRepository _repository = repository;

    public Task<Result<IQueryable<CallbackResumeViewModel>>> Handle(CallbackCollectionByUserIdQuery request, CancellationToken cancellationToken)
    {
        var selecteds =
            _repository.GetAll(c => c.CallbackOwnerId == request.UserId)
            .Select(s => new CallbackResumeViewModel
            {
                Id = s.Id,
                ItemId = s.ItemId,
                ItemPrice = s.ItemPrice,
                ServerName = s.Server!.Name,
                StoreType = s.StoreType.ToString(),
            });

        return Result.Of(selecteds).AsTask();
    }
}
