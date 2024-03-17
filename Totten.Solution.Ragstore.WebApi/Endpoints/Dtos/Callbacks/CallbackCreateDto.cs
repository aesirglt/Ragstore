namespace Totten.Solution.Ragstore.WebApi.Endpoints.Dtos.Callbacks;

public class CallbackCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string Server { get; set; } = string.Empty;
    public Dictionary<string, double> ItemByValue { get; set; }
}
