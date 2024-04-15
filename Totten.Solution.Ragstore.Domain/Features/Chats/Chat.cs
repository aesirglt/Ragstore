namespace Totten.Solution.Ragstore.Domain.Features.Chats;

using Totten.Solution.Ragstore.Domain.Bases;
using Totten.Solution.Ragstore.Domain.Features.ItemsAggregation;

public class Chat : Entity<Chat, int>
{
    public int AccountId { get; set; }
    public int CharacterId { get; set; }
    public int Limit { get; set; }
    public int IsPublic { get; set; }
    public string Map { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public int QuantityUsers { get; set; }
    public virtual List<EquipmentItem> EquipmentItems { get; set; } = new();
}