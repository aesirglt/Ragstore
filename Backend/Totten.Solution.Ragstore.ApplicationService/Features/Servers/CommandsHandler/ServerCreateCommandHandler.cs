namespace Totten.Solution.Ragstore.ApplicationService.Features.Servers.CommandsHandler;

using AutoMapper;
using LanguageExt.Common;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.Servers.Commands;
using Totten.Solution.Ragstore.Domain.Features.Servers;
using Unit = LanguageExt.Unit;

public class ServerCreateCommandHandler : IRequestHandler<ServerCreateCommand, Result<Unit>>
{
    private IMapper _mapper;
    private IServerRepository _serverRepository;
    public ServerCreateCommandHandler(IServerRepository serverRepository, IMapper mapper)
    {
        _serverRepository = serverRepository;
        _mapper = mapper;
    }
    public async Task<Result<Unit>> Handle(ServerCreateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var server = _mapper.Map<Server>(request);
            _ = await _serverRepository.Save(server);
            return await Task.FromResult(new Unit());
        }
        catch (Exception e)
        {
            return new Result<Unit>(e);
        }
    }
}
