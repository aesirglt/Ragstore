namespace Totten.Solution.RagnaComercio.ApplicationService.ViewModels.Stores;

public class SearchedItemViewModel
{
    public int ItemId { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public long Quantity { get; set; }
    public long Average { get; set; }
}
