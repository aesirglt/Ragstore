﻿namespace Totten.Solution.RagnaComercio.Domain.Features.ItemsAggregation;

using Totten.Solution.RagnaComercio.Domain.Bases;

public class EquipmentItemCardInfo
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public EquipmentItemCardInfo(string str)
    {
        var splited = str.Split(':', StringSplitOptions.RemoveEmptyEntries);
        Id = int.Parse(splited[0]);
        Name = splited[1];
    }
    /// <summary>
    /// 
    /// </summary>
    public EquipmentItemCardInfo()
    { }
}
public class EquipmentItemOptionInfo
{
    public int Id { get; set; }
    public int Val { get; set; }
    public int Param { get; set; }
    public string Name { get; set; } = string.Empty;
    public EquipmentItemOptionInfo(string str)
    {
        var splited = str.Split(':', StringSplitOptions.RemoveEmptyEntries);
        Id = int.Parse(splited[0]);
        Val = int.Parse(splited[1]);
        Param = int.Parse(splited[2]);
        Name = splited[3];
    }
    public EquipmentItemOptionInfo()
    { }
}

public record EquipmentItem : Entity<EquipmentItem, int>
{
    public int AccountId { get; set; }
    public int CharacterId { get; set; }
    public int ItemId { get; set; }
    public int ChatId { get; set; }
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

    public virtual EquipmentItemCardInfo[] InfoCards { get; set; } = [];
    public virtual EquipmentItemOptionInfo[] InfoOptions { get; set; } = [];

    public int? CrafterId { get; set; }
    public string? CrafterName { get; set; }
}
