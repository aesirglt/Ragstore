namespace Totten.Solution.RagnaComercio.ApplicationService.Features.ItemsAggregation.Queries;

using FunctionalConcepts.Results;
using MediatR;
using Totten.Solution.RagnaComercio.Domain.Features.CallbackAggregation;

public class CallbackCollectionQuery : IRequest<Result<IQueryable<Callback>>>
{
}
