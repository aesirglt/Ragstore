namespace Totten.Solution.Ragstore.ApplicationService.Features.ItemsAggregation.Queries;

using FunctionalConcepts.Results;using FunctionalConcepts;
using MediatR;
using Totten.Solution.Ragstore.Domain.Features.ItemsAggregation;

public class ItemCollectionByNameQuery : IRequest<Result<IQueryable<Item>>>
{
    public string Name { get; set; } = string.Empty;
}
