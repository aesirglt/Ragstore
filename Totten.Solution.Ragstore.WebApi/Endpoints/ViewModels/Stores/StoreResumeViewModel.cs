namespace Totten.Solution.Ragstore.WebApi.Endpoints.ViewModels.Stores;

public record StoreResumeViewModel
{
    public string Guid { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
}
