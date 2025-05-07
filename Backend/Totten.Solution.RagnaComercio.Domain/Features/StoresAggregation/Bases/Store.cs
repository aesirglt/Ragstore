namespace Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Bases;
using Totten.Solution.RagnaComercio.Domain.Bases;
using Totten.Solution.RagnaComercio.Domain.Features.Characters;

public record Store<TStore> : Entity<TStore, int>
    where TStore : Entity<TStore, int>
{
    public int AccountId { get; set; }
    public int CharacterId { get; set; }
    public string Map { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public DateTime? ExpireDate { get; set; }
    public virtual Character? Character { get; set; }
}