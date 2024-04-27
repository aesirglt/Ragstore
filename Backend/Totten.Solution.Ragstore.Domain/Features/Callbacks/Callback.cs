namespace Totten.Solution.Ragstore.Domain.Features.Callbacks;
using Totten.Solution.Ragstore.Domain.Bases;

public record Callback : Entity<Callback, int>
{
    public string Server { get; set; } = string.Empty;
    public string CallbackOwnerId { get; set; } = string.Empty;
    public string UserCellphone { get; set; } = string.Empty;
    public int ItemId { get; set; }
    public double ItemPrice { get; set; }
    public ECallbackType Level { get; set; }
}
