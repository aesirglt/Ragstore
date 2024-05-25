namespace Totten.Solution.Ragstore.Domain.Features.Servers;

using LanguageExt;
using Totten.Solution.Ragstore.Domain.Bases;
public interface IServerRepository : IRepository<Server, int>
{
    public Option<Server> GetByName(string serverName)
    {
        if (serverName is null or "") return Option<Server>.None;
        return this.GetAll(x => serverName.Equals(x.Name)).FirstOrDefault();
    }
}
