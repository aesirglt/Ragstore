namespace Totten.Solution.Ragstore.WebApi.ServicesExtension;

using Totten.Solution.Ragstore.WebApi.AppSettings;

/// <summary>
/// 
/// </summary>
public static class ConfigureAppSettings
{
    /// <summary>
    /// 
    /// </summary>
    public static IServiceCollection ConfigureAppSettingsClass(this IServiceCollection services, IConfigurationRoot configuration)
    {
        services
            .Configure<HttpClientSettings>(configuration.GetSection("HttpClients"));

        return services;
    }
}
