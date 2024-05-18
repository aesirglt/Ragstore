namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.ResponseModels;
public record StoreItemResponseModel
{
    public int Id { get; set; }
    public int StoreId { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public double Price { get; set; }
    public int Quantity { get; set; }
    public string Map { get; set; } = string.Empty;
    public string StoreName { get; set; } = string.Empty;
    public string VendingType { get; set; } = string.Empty;
    public string CharacterName { get; set; } = string.Empty;
}
