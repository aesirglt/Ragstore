namespace Totten.Solution.RagnaComercio.ApplicationService.Features.ItemsAggregation.Queries;

using FunctionalConcepts.Results;
using MediatR;
using Totten.Solution.RagnaComercio.Domain.Features.ItemsAggregation;

public class ItemCollectionByNameQuery : IRequest<Result<IQueryable<Item>>>
{
    public string Name { get; set; } = string.Empty;
}
