namespace Totten.Solution.Ragstore.Domain.Features.ItemsAggregation;
using Totten.Solution.Ragstore.Domain.Bases;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Buyings;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;

public record Item : Entity<Item, int>
{
    public string? Type { get; set; }
    public string? SubType { get; set; }
    public int? Slots { get; set; }
    public string? Description { get; set; }
    public virtual List<VendingStoreItem> VendingStoreItems { get; set; } = new();
    public virtual List<BuyingStoreItem> BuyingStoreItems { get; set; } = new();
    public virtual List<EquipmentItem> EquipmentItems { get; set; } = new();
    //public StatisticItem StatisticItem { get; set; }
}
