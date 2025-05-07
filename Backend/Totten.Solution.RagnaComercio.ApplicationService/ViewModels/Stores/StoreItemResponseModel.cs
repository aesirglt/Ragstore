namespace Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.ResponseModels;
public record StoreItemResponseModel
{
    public int ItemId { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public double Price { get; set; }
    public long Quantity { get; set; }
    public string Image { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string StoreType { get; set; } = string.Empty;

    public StoreItemResponseModel()
    {
        
    }

    public StoreItemResponseModel(StoreItemResponseModel storeItemResponseModel)
    {
        ItemId = storeItemResponseModel.ItemId;
        ItemName = storeItemResponseModel.ItemName;
        Price = storeItemResponseModel.Price;
        Quantity = storeItemResponseModel.Quantity;
        Image = storeItemResponseModel.Image;
        Category = storeItemResponseModel.Category;
        StoreType = storeItemResponseModel.StoreType;
    }
}