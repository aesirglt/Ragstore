namespace Totten.Solution.Ragstore.Domain.Features.CallbackAggregation;
using Totten.Solution.Ragstore.Domain.Bases;
using Totten.Solution.Ragstore.Domain.Features.Servers;

public record Callback : Entity<Callback, int>
{
    public int ServerId { get; set; }
    public Guid CallbackOwnerId { get; set; }
    public int ItemId { get; set; }
    public double ItemPrice { get; set; }
    public EStoreCallbackType StoreType { get; set; }
    public virtual Server? Server { get; set; }
}
