namespace Totten.Solution.Ragstore.ApplicationService.Features.ItemsAggregation.Queries;

using MediatR;
using Totten.Solution.Ragstore.Domain.Features.CallbackAggregation;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public class CallbackCollectionQuery : IRequest<Result<Exception, IQueryable<Callback>>>
{
}
