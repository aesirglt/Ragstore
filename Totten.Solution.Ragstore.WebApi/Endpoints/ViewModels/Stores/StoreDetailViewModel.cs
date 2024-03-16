namespace Totten.Solution.Ragstore.WebApi.Endpoints.ViewModels.Stores;

public record StoreDetailViewModel
{
    public string Guid { get; set; } = string.Empty;
    public string Server { get; set; } = string.Empty;
    public string Mercant { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; }
    public Dictionary<string, double> Items { get; set; } = new Dictionary<string, double>();
}
