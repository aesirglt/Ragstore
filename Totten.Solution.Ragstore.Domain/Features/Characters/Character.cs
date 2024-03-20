namespace Totten.Solution.Ragstore.Domain.Features.Characters;

using System;
using Totten.Solution.Ragstore.Domain.Bases;
using Totten.Solution.Ragstore.Domain.Features.Accounts;
using Totten.Solution.Ragstore.Domain.Features.Chats;
using Totten.Solution.Ragstore.Domain.Features.Items;
using Totten.Solution.Ragstore.Domain.Features.Stores;

public class Character : Entity<Character>
{
    public string AccountId { get; set; }
    public int JobId { get; set; }
    public int baseLevel { get; set; }
    public int Sex { get; set; }
    public string PartyName { get; set; }
    public int GuildId { get; set; }
    public string GuildName { get; set; }
    public string GuildPosition { get; set; }
    public int TitleId { get; set; }
    public string TitleName { get; set; }
    public int HairId { get; set; }
    public int HairColorId { get; set; }
    public int ClothesColorId { get; set; }
    public int WeaponId { get; set; }
    public int ShieldId { get; set; }
    public int HeadTopId { get; set; }
    public int HeadMiddleId { get; set; }
    public int HeadBottomId { get; set; }
    public int RobeId { get; set; }
    public string Map { get; set; }
    public string Location { get; set; }
    public Account Account { get; set; }
    public List<Store> Store { get; set; }
    public List<BuyingStore> BuyingStore { get; set; }
    public List<StoreItem> BuyingStoreItem { get; set; }
    public List<Chat> Chat { get; set; }
    public List<StoreItem> VendingStoreItem { get; set; }
    public List<EquipmentItem> EquipmentItem { get; set; }
}
