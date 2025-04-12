namespace Totten.Solution.Ragstore.ApplicationService.ViewModels.Callbacks;

using Totten.Solution.Ragstore.Domain.Features.CallbackAggregation;

/// <summary>
/// 
/// </summary>
public class CallbackResumeViewModel
{
    public string Server { get; set; } = string.Empty;
    public int ItemId { get; set; }
    public double ItemPrice { get; set; }
    public string StoreType { get; set; } = string.Empty;
}
