namespace Totten.Solution.Ragstore.ApplicationService.Features.Servers.CommandsHandler;

using LanguageExt;
using LanguageExt.Common;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.Servers.Commands;
using Totten.Solution.Ragstore.Domain.Features.Servers;
using Unit = LanguageExt.Unit;

public class ServerDeactiveCommandHandler : IRequestHandler<ServerDeactiveCommand, Result<Unit>>
{
    private IServerRepository _repository;
    public ServerDeactiveCommandHandler(IServerRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<Unit>> Handle(ServerDeactiveCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var maybeServer = await _repository.GetById(request.ServerId);

            return await maybeServer.Match(async server =>
            {
                server.IsActive = false;

                return new Result<Unit>(await _repository.Update(server));
            }, async () => await new Result<Unit>(new Exception("server not found")).AsTask());

        }
        catch (Exception ex)
        {
            return new Result<Unit>(new Exception("Error for updating server, contact admin.", ex.InnerException));
        }
    }
}
