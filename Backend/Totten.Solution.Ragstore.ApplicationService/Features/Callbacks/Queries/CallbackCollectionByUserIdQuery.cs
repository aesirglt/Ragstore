namespace Totten.Solution.Ragstore.ApplicationService.Features.ItemsAggregation.Queries;

using FunctionalConcepts.Results;
using MediatR;
using Totten.Solution.Ragstore.Domain.Features.CallbackAggregation;

public class CallbackCollectionByUserIdQuery : IRequest<Result<IQueryable<Callback>>>
{
    public Guid UserId { get; set; }
}
