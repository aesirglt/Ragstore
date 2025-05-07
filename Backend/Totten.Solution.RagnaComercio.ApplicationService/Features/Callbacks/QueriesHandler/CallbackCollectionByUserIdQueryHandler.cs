namespace Totten.Solution.RagnaComercio.ApplicationService.Features.Callbacks.QueriesHandler;

using FunctionalConcepts.Results;
using MediatR;
using System.Threading.Tasks;
using Totten.Solution.RagnaComercio.ApplicationService.Features.ItemsAggregation.Queries;
using Totten.Solution.RagnaComercio.ApplicationService.ViewModels.Callbacks;
using Totten.Solution.RagnaComercio.Domain.Features.CallbackAggregation;
using Totten.Solution.RagnaComercio.Infra.Cross.Statics;

public class CallbackCollectionByUserIdQueryHandler(ICallbackRepository repository)
    : IRequestHandler<CallbackCollectionByUserIdQuery, Result<IQueryable<CallbackResumeViewModel>>>
{
    private ICallbackRepository _repository = repository;

    public Task<Result<IQueryable<CallbackResumeViewModel>>> Handle(CallbackCollectionByUserIdQuery request, CancellationToken cancellationToken)
    {
        var selecteds =
            _repository.GetAll(c => c.UserId == request.UserId)
            .Select(s => new CallbackResumeViewModel
            {
                Id = s.Id,
                ItemId = s.ItemId,
                ItemPrice = s.ItemPrice,
                ServerName = s.Server!.Name,
                StoreType = s.StoreType.ToString(),
                ItemUrl = $"https://static.divine-pride.net/images/items/item/{s.ItemId}.png"
            });

        return Result.Of(selecteds).AsTask();
    }
}
