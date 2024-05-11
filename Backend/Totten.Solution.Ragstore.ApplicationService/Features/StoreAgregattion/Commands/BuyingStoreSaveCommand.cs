namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Commands;

using MediatR;
using System;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Commons;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;
using Unit = Infra.Cross.Functionals.Unit;

public class BuyingStoreSaveCommand : IRequest<Result<Exception, Unit>>
{
    public string Server { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string CharacterName { get; set; } = string.Empty;
    public int AccountId { get; set; }
    public int CharacterId { get; set; }
    public string Map { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public DateTime? ExpireDate { get; set; }
    public List<BuyingStoreItemCommand> VendingStoreItems { get; set; } = new();
}
