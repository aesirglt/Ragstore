namespace Totten.Solution.Ragstore.Domain.Features.Items;
using Totten.Solution.Ragstore.Domain.Bases;
using Totten.Solution.Ragstore.Domain.Features.Stores;

public class Item : Entity<Item>
{
    public string? Type { get; set; }
    public string? SubType { get; set; }
    public int slots { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public StoreItem StoreItem { get; set; }
    public string BuyingStoreItem { get; set; }
    public string EquipmentItem { get; set; }
    public string StatisticItem { get; set; }
}
