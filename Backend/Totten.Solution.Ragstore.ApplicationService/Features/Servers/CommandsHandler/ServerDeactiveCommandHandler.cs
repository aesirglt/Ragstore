namespace Totten.Solution.Ragstore.ApplicationService.Features.Servers.CommandsHandler;

using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.Servers.Commands;
using Totten.Solution.Ragstore.Domain.Features.Servers;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;
using Unit = Infra.Cross.Functionals.Unit;

public class ServerDeactiveCommandHandler : IRequestHandler<ServerDeactiveCommand, Result<Exception, Unit>>
{
    private IServerRepository _repository;
    public ServerDeactiveCommandHandler(IServerRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<Exception, Unit>> Handle(ServerDeactiveCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var server = await _repository.GetById(request.ServerId);

            if (server is null) return new Exception("server not found");

            server.IsActive = false;

            return await _repository.Update(server);
        }
        catch (Exception ex)
        {
            return new Exception("Error for updating server, contact admin.", ex.InnerException);
        }
    }
}
