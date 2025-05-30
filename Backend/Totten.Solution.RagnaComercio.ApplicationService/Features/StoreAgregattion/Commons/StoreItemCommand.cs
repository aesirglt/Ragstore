﻿namespace Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.Commons;
using System.Collections.Generic;

public class StoreItemCommand
{
    public string Name { get; set; } = string.Empty;
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
    public Dictionary<int, string> InfoCards { get; set; } = [];
    public Dictionary<int, InfoOptionStoreItemCommand> InfoOptions { get; set; } = [];
    public int? CrafterId { get; set; }
    public string? CrafterName { get; set; }
}