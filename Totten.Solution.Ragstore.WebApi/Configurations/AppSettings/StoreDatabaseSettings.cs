namespace Totten.Solution.Ragstore.WebApi.Configurations.AppSettings;

public class StoreDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string StoreCollectionName { get; set; } = null!;
    public string ItemCollectionName { get; set; } = null!;
}