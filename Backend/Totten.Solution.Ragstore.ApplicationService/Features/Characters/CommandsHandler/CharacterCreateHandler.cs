namespace Totten.Solution.Ragstore.ApplicationService.Features.Characters.CommandsHandler;
using FunctionalConcepts;
using FunctionalConcepts.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.Servers.Commands;

public class CharacterCreateHandler : IRequestHandler<ServerCreateCommand, Result<Success>>
{
    public Task<Result<Success>> Handle(ServerCreateCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
