namespace Totten.Solution.RagnaComercio.Domain.Features.CallbackAggregation;
using Totten.Solution.RagnaComercio.Domain.Bases;

public enum DestinationType
{
    None,
    Discord,
    Whatsapp
}
public record CallbackSchedule : Entity<CallbackSchedule, Guid>
{
    public Guid CallbackId { get; set; }
    public bool Sended { get; set; }
    public string Contact { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public DestinationType Destination { get; set; }
    public virtual Callback Callback { get; set; } = null!;
}
