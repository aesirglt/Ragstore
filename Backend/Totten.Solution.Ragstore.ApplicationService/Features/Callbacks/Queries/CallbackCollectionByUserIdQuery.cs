namespace Totten.Solution.Ragstore.ApplicationService.Features.ItemsAggregation.Queries;

using LanguageExt.Common;
using MediatR;
using Totten.Solution.Ragstore.Domain.Features.CallbackAggregation;

public class CallbackCollectionByUserIdQuery : IRequest<Result<IQueryable<Callback>>>
{
    public string UserId { get; set; } = string.Empty;
}
