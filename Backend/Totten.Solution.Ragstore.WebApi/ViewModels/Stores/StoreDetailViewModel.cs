namespace Totten.Solution.Ragstore.WebApi.ViewModels.Stores;

/// <summary>
/// 
/// </summary>
public record StoreDetailViewModel
{
    /// <summary>
    /// 
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int AccountId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string Character { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string Map { get; set; } = string.Empty;
    /// <summary>
    /// 
    /// </summary>
    public string Location { get; set; } = string.Empty;
    /// <summary>
    /// 
    /// </summary>
    public DateTime? ExpireDate { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public Dictionary<int, ItemDetail> Items { get; set; } = new();

    /// <summary>
    /// 
    /// </summary>
    public record ItemDetail
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; } = "";
        /// <summary>
        /// 
        /// </summary>
        public double Price { get; set; }
    }
}
