namespace Totten.Solution.Ragstore.ApplicationService.Features.Characters.QueriesHandler;
using FunctionalConcepts.Results;

using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.Characters.Queries;
using Totten.Solution.Ragstore.Domain.Features.Characters;

public class CharacterCollectionHandler() : IRequestHandler<CharacterCollectionQuery, Result<IQueryable<Character>>>
{
    public Task<Result<IQueryable<Character>>> Handle(CharacterCollectionQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
