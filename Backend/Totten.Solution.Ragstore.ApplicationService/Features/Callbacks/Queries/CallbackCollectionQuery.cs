namespace Totten.Solution.Ragstore.ApplicationService.Features.ItemsAggregation.Queries;

using FunctionalConcepts.Results;using FunctionalConcepts;
using MediatR;
using Totten.Solution.Ragstore.Domain.Features.CallbackAggregation;

public class CallbackCollectionQuery : IRequest<Result<IQueryable<Callback>>>
{
}
