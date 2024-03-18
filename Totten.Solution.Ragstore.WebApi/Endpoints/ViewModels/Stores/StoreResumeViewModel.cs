namespace Totten.Solution.Ragstore.WebApi.Endpoints.ViewModels.Stores;

/// <summary>
/// 
/// </summary>
public record StoreResumeViewModel
{
    /// <summary>
    /// 
    /// </summary>
    public string Guid { get; set; } = string.Empty;
    /// <summary>
    /// 
    /// </summary>
    public string Title { get; set; } = string.Empty;
    /// <summary>
    /// 
    /// </summary>
    public string Location { get; set; } = string.Empty;
}
