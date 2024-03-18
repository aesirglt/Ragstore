namespace Totten.Solution.Ragstore.WebApi.Endpoints.Dtos.Stores;
/// <summary>
/// 
/// </summary>
public record StoreCreateDto
{
    /// <summary>
    /// 
    /// </summary>
    public string Server { get; set; } = string.Empty;
    /// <summary>
    /// 
    /// </summary>
    public string SellerName { get; set; } = string.Empty;
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
    public Dictionary<string, double> Items { get; set; } = new Dictionary<string, double>();
}
