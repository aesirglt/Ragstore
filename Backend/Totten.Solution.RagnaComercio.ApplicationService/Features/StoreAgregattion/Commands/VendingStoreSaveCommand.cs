namespace Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.Commands;

using FunctionalConcepts;
using FunctionalConcepts.Results;
using MediatR;
using System;
using Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.Commons;

public class VendingStoreSaveCommand : IRequest<Result<Success>>
{
    public string Name { get; set; } = string.Empty;
    public string CharacterName { get; set; } = string.Empty;
    public int AccountId { get; set; }
    public int CharacterId { get; set; }
    public string Map { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public DateTime? ExpireDate { get; set; }
    public List<VendingStoreItemCommand> StoreItems { get; set; } = [];
}

