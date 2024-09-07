namespace Totten.Solution.Ragstore.WebApi.Dtos.Callbacks;

/// <summary>
/// 
/// </summary>
public class CallbackCreateDto
{
    /// <summary>
    /// 
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// 
    /// </summary>
    public string Server { get; set; } = string.Empty;
    /// <summary>
    /// 
    /// </summary>
    public int ItemId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public double ItemPrice { get; set; }
}
