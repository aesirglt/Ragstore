namespace Totten.Solution.Ragstore.ApplicationService.Features.ItemsAggregation.Queries;

using LanguageExt.Common;
using MediatR;
using Totten.Solution.Ragstore.Domain.Features.CallbackAggregation;

public class CallbackCollectionQuery : IRequest<Result<IQueryable<Callback>>>
{
}
