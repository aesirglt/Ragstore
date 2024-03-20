namespace Totten.Solution.Ragstore.Domain.Features.Accounts;

using Totten.Solution.Ragstore.Domain.Bases;
using Totten.Solution.Ragstore.Domain.Features.Characters;
using Totten.Solution.Ragstore.Domain.Features.Chats;
using Totten.Solution.Ragstore.Domain.Features.Items;
using Totten.Solution.Ragstore.Domain.Features.Stores;
using Totten.Solution.Ragstore.Domain.Features.Users;

public class Account : Entity<Account>
{
    public int UserId { get; set; }
    public bool IsReported { get; set; }
    public UserStore? UserStore { get; set; }
    public Character Character { get; set; }
    public BuyingStore BuyingStore { get; set; }
    public Chat Chat { get; set; }
    public List<Store> Stores { get; set; }
    public List<StoreItem> StoreItems { get; set; }
    public List<StoreItem> BuyingStoreItems { get; set; }
    public List<EquipmentItem> EquipmentItems { get; set; }
}
