namespace Totten.Solution.Ragstore.Domain.Features.Accounts;

using Totten.Solution.Ragstore.Domain.Bases;
using Totten.Solution.Ragstore.Domain.Features.Characters;
using Totten.Solution.Ragstore.Domain.Features.Chats;
using Totten.Solution.Ragstore.Domain.Features.ItemAgreggation;
using Totten.Solution.Ragstore.Domain.Features.StoresAgreggation.Buyings;
using Totten.Solution.Ragstore.Domain.Features.StoresAgreggation.Vendings;
using Totten.Solution.Ragstore.Domain.Features.Users;

public class Account : Entity<Account, int>
{
    public int AccountId { get; set; }
    public int? UserId { get; set; }
    public bool IsReported { get; set; }
    public User? User { get; set; }
    public List<Character> Characters { get; set; } = new();
    public List<Chat> Chats { get; set; } = new();
    public List<VendingStore> VendingStores { get; set; } = new();
    public List<BuyingStore> BuyingStores { get; set; } = new();
    public List<VendingStoreItem> VendingStoreItems { get; set; } = new();
    public List<BuyingStoreItem> BuyingStoreItems { get; set; } = new();
    public List<EquipmentItem> EquipmentItems { get; set; } = new();
}
