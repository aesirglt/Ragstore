namespace Totten.Solution.Ragstore.ApplicationService.Features.ItemsAggregation.Queries;

using LanguageExt.Common;
using MediatR;
using Totten.Solution.Ragstore.Domain.Features.ItemsAggregation;

public class ItemCollectionByNameQuery : IRequest<Result<IQueryable<Item>>>
{
    public string Name { get; set; } = string.Empty;
}
