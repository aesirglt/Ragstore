namespace Totten.Solution.Ragstore.ApplicationService.Features.ItemsAggregation.Queries;

using FunctionalConcepts.Results;using FunctionalConcepts;
using MediatR;
using Totten.Solution.Ragstore.Domain.Features.CallbackAggregation;

public class CallbackCollectionByUserIdQuery : IRequest<Result<IQueryable<Callback>>>
{
    public string UserId { get; set; } = string.Empty;
}
