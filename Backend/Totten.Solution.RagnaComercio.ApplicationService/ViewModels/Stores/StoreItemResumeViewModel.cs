namespace Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.ResponseModels;
public record StoreItemResumeViewModel
{
    public int ItemId { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public long Quantity { get; set; }
    public string Image { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;

    public StoreItemResumeViewModel()
    {
        
    }

    public StoreItemResumeViewModel(StoreItemResumeViewModel storeItemResponseModel)
    {
        ItemId = storeItemResponseModel.ItemId;
        ItemName = storeItemResponseModel.ItemName;
        Quantity = storeItemResponseModel.Quantity;
        Image = storeItemResponseModel.Image;
        Category = storeItemResponseModel.Category;
    }
}