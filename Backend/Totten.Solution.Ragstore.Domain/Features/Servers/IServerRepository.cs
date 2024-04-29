namespace Totten.Solution.Ragstore.Domain.Features.Servers;
using Totten.Solution.Ragstore.Domain.Bases;
using Totten.Solution.Ragstore.Infra.Cross.Errors.EspecifiedErrors;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public interface IServerRepository : IRepository<Server, int>
{
    public Result<Exception, Server> GetByName(string serverName)
    {
        try
        {
            if (serverName is null or "")
                return new InvalidObjectError("ServerName não fo informado.");

            var server = this.GetAllByFilter(x => serverName.Equals(x.Name)).FirstOrDefault();

            return server is null ? new NotFoundError("Servidor não encontrado") : server;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
}
