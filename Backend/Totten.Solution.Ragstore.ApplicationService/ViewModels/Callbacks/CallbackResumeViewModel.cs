namespace Totten.Solution.Ragstore.ApplicationService.ViewModels.Callbacks;
/// <summary>
/// 
/// </summary>
public record CallbackResumeViewModel
{
    public int Id { get; set; }
    public int ItemId { get; set; }
    public double ItemPrice { get; set; }
    public string ServerName { get; set; } = string.Empty;
    public string StoreType { get; set; } = string.Empty;
}