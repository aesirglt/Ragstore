namespace Totten.Solution.RagnaComercio.ApplicationService.ViewModels.Stores;
/// <summary>
/// 
/// </summary>
public record StoreResumeViewModel
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public int CharacterId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CharacterName { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public int ItemId { get; set; }
    public double ItemPrice { get; set; }
}
