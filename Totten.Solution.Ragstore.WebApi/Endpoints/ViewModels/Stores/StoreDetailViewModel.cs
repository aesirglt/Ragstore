namespace Totten.Solution.Ragstore.WebApi.Endpoints.ViewModels.Stores;

/// <summary>
/// 
/// </summary>
public record StoreDetailViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int AccountId { get; set; }
    public int CharacterId { get; set; }
    public string Map { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public DateTime? ExpireDate { get; set; }
    public Dictionary<int, string> Items { get; set; }
}
