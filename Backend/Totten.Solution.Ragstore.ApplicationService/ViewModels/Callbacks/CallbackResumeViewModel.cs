namespace Totten.Solution.Ragstore.ApplicationService.ViewModels.Callbacks;
/// <summary>
/// 
/// </summary>
public class CallbackResumeViewModel
{
    public int ItemId { get; set; }
    public double ItemPrice { get; set; }
    public string Server { get; set; } = string.Empty;
    public string StoreType { get; set; } = string.Empty;
}