namespace Totten.Solution.RagnaComercio.WebApi.Dtos.Stores;

using Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.Commons;

/// <summary>
/// 
/// </summary>
public class BuyingStoreSaveDto
{
    /// <summary>
    /// 
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// 
    /// </summary>
    public string CharacterName { get; set; } = string.Empty;
    /// <summary>
    /// 
    /// </summary>
    public int AccountId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int CharacterId { get; set; }
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
    public List<BuyingStoreItemCommand> StoreItems { get; set; } = [];
}
