namespace Totten.Solution.Ragstore.Domain.Features.Stores;
using Totten.Solution.Ragstore.Domain.Bases;

public class Store : Entity<Store>
{
    public string Server { get; set; } = string.Empty;
    public string Mercant { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Items { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; }
}
