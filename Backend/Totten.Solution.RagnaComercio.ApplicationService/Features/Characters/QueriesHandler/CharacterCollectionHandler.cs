﻿namespace Totten.Solution.RagnaComercio.ApplicationService.Features.Characters.QueriesHandler;
using FunctionalConcepts.Results;

using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.RagnaComercio.ApplicationService.Features.Characters.Queries;
using Totten.Solution.RagnaComercio.Domain.Features.Characters;
using Totten.Solution.RagnaComercio.Infra.Cross.Statics;

public class CharacterCollectionHandler(ICharacterRepository characterRepository) : IRequestHandler<CharacterCollectionQuery, Result<IQueryable<Character>>>
{
    private readonly ICharacterRepository _characterRepository = characterRepository;

    public async Task<Result<IQueryable<Character>>> Handle(CharacterCollectionQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var characters = await _characterRepository.GetAll(x => x.UpdatedAt >= request.Server.UpdatedAt).AsTask();
            return Result.Of(characters);
        }
        catch (Exception)
        {
            return await Result.Of(Array.Empty<Character>().AsQueryable()).AsTask();
        }
    }
}
