namespace Totten.Solution.Ragstore.Domain.Features.CallbackAggregation;
using Totten.Solution.Ragstore.Domain.Bases;
using Totten.Solution.Ragstore.Domain.Features.Servers;
using Totten.Solution.Ragstore.Domain.Features.Users;

public record Callback : Entity<Callback, int>
{
    public int ServerId { get; set; }
    public Guid UserId { get; set; }
    public int ItemId { get; set; }
    public double ItemPrice { get; set; }
    public EStoreCallbackType StoreType { get; set; }
    public virtual Server? Server { get; set; }
    public virtual User? User { get; set; }
}
