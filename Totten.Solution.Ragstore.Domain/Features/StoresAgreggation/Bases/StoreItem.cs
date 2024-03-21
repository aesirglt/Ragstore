namespace Totten.Solution.Ragstore.Domain.Features.StoresAgreggation.Bases;
using Totten.Solution.Ragstore.Domain.Bases;
using Totten.Solution.Ragstore.Domain.Features.Accounts;
using Totten.Solution.Ragstore.Domain.Features.Characters;
using Totten.Solution.Ragstore.Domain.Features.ItemAgreggation;
using Totten.Solution.Ragstore.Domain.Features.StoresAgreggation.Vendings;

public class StoreItem : Entity<VendingStoreItem>
{
    public int AccountId { get; set; }
    public int CharacterId { get; set; }
    public int ItemId { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public int Type { get; set; }
    public int Refine { get; set; }
    public int? EnchantGrade { get; set; }
    public int IsIdentified { get; set; }
    public int IsDamaged { get; set; }
    public int? Location { get; set; }
    public int? SpriteId { get; set; }
    public int Slots { get; set; }
    public int? Card0Id { get; set; }
    public string? Card0Name { get; set; }
    public int? Card1Id { get; set; }
    public string? Card1Name { get; set; }
    public int? Card2Id { get; set; }
    public string? Card2Name { get; set; }
    public int? Card3Id { get; set; }
    public string? Card3Name { get; set; }
    public int? Option0Id { get; set; }
    public int? Option0Val { get; set; }
    public int? Option0Param { get; set; }
    public string? Option0Name { get; set; }
    public int? Option1Id { get; set; }
    public int? Option1Val { get; set; }
    public int? Option1Param { get; set; }
    public string? Option1Name { get; set; }
    public int? Option2Id { get; set; }
    public int? Option2Val { get; set; }
    public int? Option2Param { get; set; }
    public string? Option2Name { get; set; }
    public int? Option3Id { get; set; }
    public int? Option3Val { get; set; }
    public int? Option3Param { get; set; }
    public string? Option3Name { get; set; }
    public int? Option4Id { get; set; }
    public int? Option4Val { get; set; }
    public int? Option4Param { get; set; }
    public string? Option4Name { get; set; }
    public int? CrafterId { get; set; }
    public string? CrafterName { get; set; }
    public Account Account { get; set; }
    public Item Item { get; set; }
    public Character Character { get; set; }
}
