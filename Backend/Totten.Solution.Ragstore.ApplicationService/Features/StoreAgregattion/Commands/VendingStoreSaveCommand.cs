namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Commands;

using FunctionalConcepts.Results;using FunctionalConcepts;
using MediatR;
using System;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Commons;


public class VendingStoreSaveCommand : IRequest<Result<Success>>
{
    public string Server { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string CharacterName { get; set; } = string.Empty;
    public int AccountId { get; set; }
    public int CharacterId { get; set; }
    public string Map { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public DateTime? ExpireDate { get; set; }
    public List<VendingStoreItemCommand> StoreItems { get; set; } = new();
}

