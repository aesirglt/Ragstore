namespace Totten.Solution.Ragstore.WebApi.Dtos.Callbacks;
using ItemId = int;
using ItemPrice = double;
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
    public Dictionary<ItemId, ItemPrice> ItemByValue { get; set; } = new Dictionary<ItemId, ItemPrice>();
}
