namespace Totten.Solution.RagnaComercio.Domain.Features.CallbackAggregation;
using Totten.Solution.RagnaComercio.Domain.Bases;
using Totten.Solution.RagnaComercio.Domain.Features.Servers;
using Totten.Solution.RagnaComercio.Domain.Features.Users;

public record Callback : Entity<Callback, Guid>
{
    public Guid ServerId { get; set; }
    public Guid UserId { get; set; }
    public int ItemId { get; set; }
    public double ItemPrice { get; set; }
    public EStoreCallbackType StoreType { get; set; }
    public virtual Server? Server { get; set; }
    public virtual User? User { get; set; }
    public virtual List<CallbackSchedule> CallbackSchedules { get; set; } = [];
}
