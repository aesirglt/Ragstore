namespace Totten.Solution.Ragstore.ApplicationService.Features.ItemsAggregation.Queries;

using MediatR;
using Totten.Solution.Ragstore.Domain.Features.ItemsAggregation;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public class ItemCollectionByNameQuery : IRequest<Result<Exception, List<Item>>>
{
    public string Name { get; set; } = string.Empty;
}
