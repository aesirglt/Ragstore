namespace Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Bases;
using Totten.Solution.Ragstore.Domain.Bases;
public class StoreItemCardInfo
{
    public int Id { get; set; }
    public string Name { get; set; }

    public StoreItemCardInfo(string str)
    {
        var splited = str.Split(':', StringSplitOptions.RemoveEmptyEntries);
        Id = int.Parse(splited[0]);
        Name = splited[1];
    }
    public StoreItemCardInfo() : this(0, string.Empty)
    {
    }
    public StoreItemCardInfo(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
public class StoreItemOptionInfo
{
    public int Id { get; set; }
    public int Val { get; set; }
    public int Param { get; set; }
    public string Name { get; set; }
    public StoreItemOptionInfo(string str)
    {
        var splited = str.Split(':', StringSplitOptions.RemoveEmptyEntries);
        Id = int.Parse(splited[0]);
        Val = int.Parse(splited[1]);
        Param = int.Parse(splited[2]);
        Name = splited[3];
    }
    public StoreItemOptionInfo(int id, int val, int param, string str)
    {
        Id = id;
        Val = val;
        Param = param;
        Name = str;
    }
    public StoreItemOptionInfo() : this(0, 0, 0, string.Empty)
    { }
}
public abstract record StoreItem<TStoreItem> : Entity<TStoreItem, int>
    where TStoreItem : StoreItem<TStoreItem>
{
    public string Map { get; set; } = string.Empty;
    public string StoreName { get; set; } = string.Empty;
    public string CharacterName { get; set; } = string.Empty;
    public int StoreId { get; set; }
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

    public virtual StoreItemCardInfo[] InfoCards { get; set; } = Array.Empty<StoreItemCardInfo>();
    public virtual StoreItemOptionInfo[] InfoOptions { get; set; } = Array.Empty<StoreItemOptionInfo>();
    public int? CrafterId { get; set; }
    public string? CrafterName { get; set; }
}
