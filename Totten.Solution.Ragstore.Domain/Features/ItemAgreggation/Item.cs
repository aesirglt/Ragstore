namespace Totten.Solution.Ragstore.Domain.Features.ItemAgreggation;
using Totten.Solution.Ragstore.Domain.Bases;
using Totten.Solution.Ragstore.Domain.Features.StoresAgreggation.Buyings;
using Totten.Solution.Ragstore.Domain.Features.StoresAgreggation.Vendings;

public class Item : Entity<Item>
{
    public string? Type { get; set; }
    public string? SubType { get; set; }
    public int? Slots { get; set; }
    public string? Description { get; set; }
    public List<VendingStoreItem> VendingStoreItems { get; set; } = new();
    public List<BuyingStoreItem> BuyingStoreItems { get; set; } = new();
    public List<BuyingStoreItem> EquipmentItems { get; set; } = new();
    //public StatisticItem StatisticItem { get; set; }
}
