namespace Totten.Solution.RagnaComercio.ApplicationService.Features.ItemsAggregation.Queries;

using FunctionalConcepts.Results;
using MediatR;
using Totten.Solution.RagnaComercio.ApplicationService.ViewModels.Callbacks;
using Totten.Solution.RagnaComercio.Domain.Features.CallbackAggregation;

public class CallbackCollectionByUserIdQuery : IRequest<Result<IQueryable<CallbackResumeViewModel>>>
{
    public Guid UserId { get; set; }
}
