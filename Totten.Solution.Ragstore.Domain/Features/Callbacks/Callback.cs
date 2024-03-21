namespace Totten.Solution.Ragstore.Domain.Features.Callbacks;
using System.Collections.Generic;
using Totten.Solution.Ragstore.Domain.Bases;

public class Callback : Entity<Callback, Guid>
{
    public string Server { get; set; } = string.Empty;
    public string CallbackOwnerId { get; set; } = string.Empty;
    public string UserCellphone { get; set; } = string.Empty;
    public Dictionary<string, double> Items { get; set; } = new Dictionary<string, double>();
    public ECallbackType Level { get; set; }
}
