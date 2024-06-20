namespace Totten.Solution.Ragstore.Domain.Features.Servers;

using FunctionalConcepts.Options;
using Totten.Solution.Ragstore.Domain.Bases;
public interface IServerRepository : IRepository<Server, int>
{
    public Option<Server> GetByName(string serverName)
    {
        if (serverName is null or "") return NoneType.Value;
        return this.GetAll(x => serverName.Equals(x.Name)).FirstOrDefault();
    }
}
