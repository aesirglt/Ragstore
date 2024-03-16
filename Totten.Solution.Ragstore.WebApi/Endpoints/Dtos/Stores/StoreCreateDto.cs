namespace Totten.Solution.Ragstore.WebApi.Endpoints.Dtos.Stores;

public record StoreCreateDto
{
    public string Server { get; set; } = string.Empty;
    public string SellerName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public Dictionary<string, double> Items { get; set; } = new Dictionary<string, double>();
}
