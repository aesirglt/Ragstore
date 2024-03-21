namespace Totten.Solution.Ragstore.Domain.Features.Characters;

using System;
using Totten.Solution.Ragstore.Domain.Bases;
using Totten.Solution.Ragstore.Domain.Features.Accounts;
using Totten.Solution.Ragstore.Domain.Features.Chats;
using Totten.Solution.Ragstore.Domain.Features.ItemAgreggation;
using Totten.Solution.Ragstore.Domain.Features.StoresAgreggation.Buyings;
using Totten.Solution.Ragstore.Domain.Features.StoresAgreggation.Vendings;

public class Character : Entity<Character, Guid>
{
    public int AccountId { get; set; }
    public int CharacterId { get; set; }
    public int JobId { get; set; }
    public int BaseLevel { get; set; }
    public int Sex { get; set; }
    public string? PartyName { get; set; }
    public int? GuildId { get; set; }
    public string? GuildName { get; set; }
    public string? GuildPosition { get; set; }
    public int? TitleId { get; set; }
    public string? TitleName { get; set; }
    public int? HairId { get; set; }
    public int? HairColorId { get; set; }
    public int? ClothesColorId { get; set; }
    public int? WeaponId { get; set; }
    public int? ShieldId { get; set; }
    public int? HeadTopId { get; set; }
    public int? HeadMiddleId { get; set; }
    public int? HeadBottomId { get; set; }
    public int? RobeId { get; set; }
    public string Map { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public Account Account { get; set; }
    public List<VendingStore> VendingStores { get; set; } = new();
    public List<BuyingStore> BuyingStores { get; set; } = new();
    public List<VendingStoreItem> BuyingStoreItems { get; set; } = new();
    public List<Chat> Chats { get; set; } = new();
    public List<VendingStoreItem> VendingStoreItems { get; set; } = new();
    public List<EquipmentItem> EquipmentItems { get; set; } = new();
}
