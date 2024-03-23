namespace Totten.Solution.Ragstore.ApplicationService.Features.ItemsAggregation.Queries;

using MediatR;
using Totten.Solution.Ragstore.Domain.Features.Callbacks;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public class CallbackCollectionByUserIdQuery : IRequest<Result<Exception, List<Callback>>>
{
    public string UserId { get; set; } = string.Empty;
}
