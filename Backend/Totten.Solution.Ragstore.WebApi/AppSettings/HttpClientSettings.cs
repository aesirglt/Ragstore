namespace Totten.Solution.Ragstore.WebApi.AppSettings;

/// <summary>
/// 
/// </summary>
public class HttpClientSettings
{
    /// <summary>
    /// 
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// 
    /// </summary>
    public string Url { get; set; } = string.Empty;
    /// <summary>
    /// 
    /// </summary>
    public Dictionary<string, string> DefaultHeaders { get; set; } = new Dictionary<string, string>();
    /// <summary>
    /// 
    /// </summary>
    public Dictionary<string, string> DefaultInQueryData { get; set; } = new Dictionary<string, string>();
}
