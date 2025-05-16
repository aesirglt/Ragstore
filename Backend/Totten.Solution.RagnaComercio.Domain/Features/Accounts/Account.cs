namespace Totten.Solution.RagnaComercio.Domain.Features.Accounts;

using Totten.Solution.RagnaComercio.Domain.Bases;
using Totten.Solution.RagnaComercio.Domain.Features.Characters;
using Totten.Solution.RagnaComercio.Domain.Features.Chats;
using Totten.Solution.RagnaComercio.Domain.Features.ItemsAggregation;
using Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Buyings;
using Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Vendings;
using Totten.Solution.RagnaComercio.Domain.Features.Users;

public record Account : Entity<Account, int>
{
    public bool IsReported { get; set; }
    public Guid? UserId { get; set; }
    public virtual User? User { get; set; }
    public virtual List<Character> Characters { get; set; } = [];
    public virtual List<Chat> Chats { get; set; } = [];
    public virtual List<VendingStore> VendingStores { get; set; } = [];
    public virtual List<BuyingStore> BuyingStores { get; set; } = [];
    public virtual List<VendingStoreItem> VendingStoreItems { get; set; } = [];
    public virtual List<BuyingStoreItem> BuyingStoreItems { get; set; } = [];
    public virtual List<EquipmentItem> EquipmentItems { get; set; } = [];
}
