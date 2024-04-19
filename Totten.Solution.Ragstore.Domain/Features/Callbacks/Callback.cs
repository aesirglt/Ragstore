namespace Totten.Solution.Ragstore.Domain.Features.Callbacks;
using System.Collections.Generic;
using Totten.Solution.Ragstore.Domain.Bases;

using ItemId = int;
using ItemPrice = double;
public record Callback : Entity<Callback, int>
{
    public string Server { get; set; } = string.Empty;
    public string CallbackOwnerId { get; set; } = string.Empty;
    public string UserCellphone { get; set; } = string.Empty;
    public Dictionary<ItemId, ItemPrice> Items { get; set; } = new ();
    public ECallbackType Level { get; set; }
}
