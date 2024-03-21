namespace Totten.Solution.Ragstore.Domain.Features.StoresAgreggation.Bases;
using Totten.Solution.Ragstore.Domain.Bases;
using Totten.Solution.Ragstore.Domain.Features.Accounts;
using Totten.Solution.Ragstore.Domain.Features.Characters;

public class Store<TStore> : Entity<TStore> where TStore : Entity<TStore>
{
    public int AccountId { get; set; }
    public int CharacterId { get; set; }
    public string Map { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public DateTime? ExpireDate { get; set; }
    public Account Account { get; set; }
    public Character Character { get; set; }
}