namespace Totten.Solution.RagnaComercio.ApplicationService.Features.Characters.Queries;

using FunctionalConcepts.Results;
using MediatR;
using System.Linq;
using Totten.Solution.RagnaComercio.Domain.Features.Characters;
using Totten.Solution.RagnaComercio.Domain.Features.Servers;

public class CharacterCollectionQuery : IRequest<Result<IQueryable<Character>>>
{
    public Server Server { get; set; }
}
