namespace Totten.Solution.Ragstore.WebApi.AppSettings;

/// <summary>
/// 
/// </summary>
public class StoreDatabaseSettings
{
    /// <summary>
    /// 
    /// </summary>
    public string ConnectionString { get; set; } = null!;
    /// <summary>
    /// 
    /// </summary>
    public string DatabaseName { get; set; } = null!;
    /// <summary>
    /// 
    /// </summary>
    public string StoreCollectionName { get; set; } = null!;
    /// <summary>
    /// 
    /// </summary>
    public string ItemCollectionName { get; set; } = null!;
    /// <summary>
    /// 
    /// </summary>
    public string CallbackCollectionName { get; set; } = null!;
}