namespace Totten.Solution.Ragstore.ApplicationService.Features.Characters.Queries;

using FunctionalConcepts.Results;
using MediatR;
using System.Linq;
using Totten.Solution.Ragstore.Domain.Features.Characters;
using Totten.Solution.Ragstore.Domain.Features.Servers;

public class CharacterCollectionQuery : IRequest<Result<IQueryable<Character>>>
{
    public Server Server { get; set; }
}
