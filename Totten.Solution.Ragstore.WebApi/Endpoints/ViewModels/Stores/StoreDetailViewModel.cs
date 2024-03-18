namespace Totten.Solution.Ragstore.WebApi.Endpoints.ViewModels.Stores;

/// <summary>
/// 
/// </summary>
public record StoreDetailViewModel
{
    /// <summary>
    /// 
    /// </summary>
    public string Guid { get; set; } = string.Empty;
    /// <summary>
    /// 
    /// </summary>
    public string Server { get; set; } = string.Empty;
    /// <summary>
    /// 
    /// </summary>
    public string Merchant { get; set; } = string.Empty;
    /// <summary>
    /// 
    /// </summary>
    public string Title { get; set; } = string.Empty;
    /// <summary>
    /// 
    /// </summary>
    public string Location { get; set; } = string.Empty;
    /// <summary>
    /// 
    /// </summary>
    public DateTime CreationDate { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public Dictionary<string, double> Items { get; set; } = new Dictionary<string, double>();
}
