﻿namespace Totten.Solution.RagnaComercio.ApplicationService.Features.Servers.CommandsHandler;

using FunctionalConcepts;
using FunctionalConcepts.Errors;
using FunctionalConcepts.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.RagnaComercio.ApplicationService.Features.Servers.Commands;
using Totten.Solution.RagnaComercio.Domain.Features.Servers;

public class ServerDeactiveCommandHandler(IServerRepository repository)
    : IRequestHandler<ServerDeactiveCommand, Result<Success>>
{
    private readonly IServerRepository _repository = repository;

    public async Task<Result<Success>> Handle(
        ServerDeactiveCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var maybeServer = await _repository.GetById(request.ServerId);

            return await maybeServer.MatchAsync(async server =>
            {
                server.IsActive = false;
                return Result.Of(await _repository.Update(server));
            }, () => NotFoundError.New("server not found"));

        }
        catch (Exception ex)
        {
            return UnhandledError.New("Error for updating server, contact the admin.", ex);
        }
    }
}
