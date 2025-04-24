namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.ResponseModels;
public record StoreItemResponseModel
{
    public int ItemId { get; set; }
    public int StoreId { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public double Price { get; set; }
    public long Quantity { get; set; }
    public string Image { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;

    public StoreItemResponseModel()
    {
        
    }

    public StoreItemResponseModel(StoreItemResponseModel storeItemResponseModel)
    {
        ItemId = storeItemResponseModel.ItemId;
        StoreId = storeItemResponseModel.StoreId;
        ItemName = storeItemResponseModel.ItemName;
        Price = storeItemResponseModel.Price;
        Quantity = storeItemResponseModel.Quantity;
        Image = storeItemResponseModel.Image;
        Category = storeItemResponseModel.Category;
    }
}