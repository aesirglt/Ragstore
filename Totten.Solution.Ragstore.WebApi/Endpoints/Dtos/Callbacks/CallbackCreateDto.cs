namespace Totten.Solution.Ragstore.WebApi.Endpoints.Dtos.Callbacks;

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
    public Dictionary<string, double> ItemByValue { get; set; } = new Dictionary<string, double>();
}
