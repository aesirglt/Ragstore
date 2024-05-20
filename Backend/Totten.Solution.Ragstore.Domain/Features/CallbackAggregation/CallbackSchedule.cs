namespace Totten.Solution.Ragstore.Domain.Features.CallbackAggregation;
using Totten.Solution.Ragstore.Domain.Bases;

public record CallbackSchedule : Entity<CallbackSchedule, int>
{
    public bool Sended { get; set; }
    public string Contact { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public DateTime SendIn { get; set; }
}
