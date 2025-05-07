namespace Totten.Solution.RagnaComercio.Domain.Features.CallbackAggregation;
using Totten.Solution.RagnaComercio.Domain.Bases;

public record CallbackSchedule : Entity<CallbackSchedule, Guid>
{
    public bool Sended { get; set; }
    public string Contact { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public DateTime SendIn { get; set; }
}
