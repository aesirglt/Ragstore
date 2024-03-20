namespace Totten.Solution.Ragstore.Domain.Features.Stores;
using Totten.Solution.Ragstore.Domain.Bases;
using Totten.Solution.Ragstore.Domain.Features.Accounts;
using Totten.Solution.Ragstore.Domain.Features.Characters;

public class BuyingStore : Entity<BuyingStore>
{
    public string AccountId { get; set; }
    public string CharacterId { get; set; }
    public string PriceLimit { get; set; }
    public string Map { get; set; }
    public string Location { get; set; }
    public string ExpireDate { get; set; }
    public Account Account { get; set; }
    public Character Character { get; set; }
    public StoreItem BuyingStoreItem { get; set; }
}