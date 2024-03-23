namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Commands;

using MediatR;
using System;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;
using Unit = Infra.Cross.Functionals.Unit;

public class VendingStoreSaveCommand : IRequest<Result<Exception, Unit>>
{
    public string Name { get; set; }
    public int AccountId { get; set; }
    public int CharacterId { get; set; }
    public string Map { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public DateTime? ExpireDate { get; set; }
    public List<VendingStoreItemCommand> VendingStoreItems { get; set; } = new();
}
public class VendingStoreItemCommand : StoreItemCommand
{
    public long? ExpireDate { get; set; }
}
public class InfoOptionStoreItemCommand
{
    public int Val { get; set; }
    public int Param { get; set; }
    public string Name { get; set; } = string.Empty;
}
public class StoreItemCommand
{
    public string Name { get; set; }
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
    public Dictionary<int, string> InfoCards { get; set; } = new();
    public Dictionary<int, InfoOptionStoreItemCommand> InfoOptions { get; set; } = new();
    public int? CrafterId { get; set; }
    public string? CrafterName { get; set; }
}

