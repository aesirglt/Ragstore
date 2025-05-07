namespace Totten.Solution.RagnaComercio.Domain.Features.Chats;

using Totten.Solution.RagnaComercio.Domain.Bases;
using Totten.Solution.RagnaComercio.Domain.Features.ItemsAggregation;

public record Chat : Entity<Chat, int>
{
    public int AccountId { get; set; }
    public int CharacterId { get; set; }
    public int Limit { get; set; }
    public int IsPublic { get; set; }
    public string Map { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public int QuantityUsers { get; set; }
    public virtual List<EquipmentItem> EquipmentItems { get; set; } = [];
}