namespace Totten.Solution.Ragstore.ApplicationService.Features.ItemsAggregation.CommandsHandler;
using FunctionalConcepts;
using FunctionalConcepts.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.ItemsAggregation.Commands;

public class ItemCreateHandler : IRequestHandler<ItemCreateCommand, Result<Success>>
{
    public Task<Result<Success>> Handle(ItemCreateCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
