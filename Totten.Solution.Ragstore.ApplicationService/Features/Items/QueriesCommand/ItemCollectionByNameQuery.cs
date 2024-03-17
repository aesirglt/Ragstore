namespace Totten.Solution.Ragstore.ApplicationService.Features.Items.QueriesCommand;

using MediatR;
using Totten.Solution.Ragstore.Domain.Features.Items;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public class ItemCollectionByNameQuery : IRequest<Result<Exception, List<Item>>>
{
    public string Name { get; set; } = string.Empty;
}
