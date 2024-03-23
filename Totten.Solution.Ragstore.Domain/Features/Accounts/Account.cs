namespace Totten.Solution.Ragstore.Domain.Features.Accounts;

using Totten.Solution.Ragstore.Domain.Bases;
using Totten.Solution.Ragstore.Domain.Features.Characters;
using Totten.Solution.Ragstore.Domain.Features.Chats;
using Totten.Solution.Ragstore.Domain.Features.ItemsAggregation;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Buyings;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;
using Totten.Solution.Ragstore.Domain.Features.Users;

public class Account : Entity<Account, int>
{
    public bool IsReported { get; set; }
    public int? UserId { get; set; }
    public virtual User? User { get; set; }
    public virtual List<Character> Characters { get; set; } = new();
    public virtual List<Chat> Chats { get; set; } = new();
    public virtual List<VendingStore> VendingStores { get; set; } = new();
    public virtual List<BuyingStore> BuyingStores { get; set; } = new();
    public virtual List<VendingStoreItem> VendingStoreItems { get; set; } = new();
    public virtual List<BuyingStoreItem> BuyingStoreItems { get; set; } = new();
    public virtual List<EquipmentItem> EquipmentItems { get; set; } = new();
}
