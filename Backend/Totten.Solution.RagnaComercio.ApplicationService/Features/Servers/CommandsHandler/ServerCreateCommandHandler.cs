﻿namespace Totten.Solution.RagnaComercio.ApplicationService.Features.Servers.CommandsHandler;

using AutoMapper;
using FunctionalConcepts;
using FunctionalConcepts.Errors;
using FunctionalConcepts.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.RagnaComercio.ApplicationService.Features.Servers.Commands;
using Totten.Solution.RagnaComercio.Domain.Features.Servers;

public class ServerCreateCommandHandler : IRequestHandler<ServerCreateCommand, Result<Success>>
{
    private IMapper _mapper;
    private IServerRepository _serverRepository;
    public ServerCreateCommandHandler(IServerRepository serverRepository, IMapper mapper)
    {
        _serverRepository = serverRepository;
        _mapper = mapper;
    }
    public async Task<Result<Success>> Handle(ServerCreateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var server = _mapper.Map<Server>(request);
            _ = await _serverRepository.Save(server);
            return await Task.FromResult(Result.Success);
        }
        catch (Exception exn)
        {
            return UnhandledError.New(exn.Message, exn);
        }
    }
}
