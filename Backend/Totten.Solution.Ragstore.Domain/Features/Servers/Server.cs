namespace Totten.Solution.Ragstore.Domain.Features.Servers;
using Totten.Solution.Ragstore.Domain.Bases;

public record Server : Entity<Server, int>, IActive
{
    public bool IsActive { get; set; }
    public string SiteUrl { get; set; }
}
