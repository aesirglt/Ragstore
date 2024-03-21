namespace Totten.Solution.Ragstore.Domain.Features.Accounts;

using Totten.Solution.Ragstore.Domain.Bases;
using Totten.Solution.Ragstore.Domain.Features.Characters;
using Totten.Solution.Ragstore.Domain.Features.Chats;
using Totten.Solution.Ragstore.Domain.Features.ItemAgreggation;
using Totten.Solution.Ragstore.Domain.Features.StoresAgreggation.Buyings;
using Totten.Solution.Ragstore.Domain.Features.StoresAgreggation.Vendings;
using Totten.Solution.Ragstore.Domain.Features.Users;

public class Account : Entity<Account, Guid>
{
    public Guid UserId { get; set; }
    public bool IsReported { get; set; }
    public User? User { get; set; }
    public Character Character { get; set; }
    public Chat Chat { get; set; }
    public List<BuyingStore> BuyingStores { get; set; } = new();
    public List<VendingStore> VendingStores { get; set; } = new();
    public List<VendingStoreItem> VendingStoreItems { get; set; } = new();
    public List<BuyingStoreItem> BuyingStoreItems { get; set; } = new();
    public List<EquipmentItem> EquipmentItems { get; set; } = new();
}
