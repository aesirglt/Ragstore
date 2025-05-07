namespace Totten.Solution.RagnaComercio.WebApi.ServicesExtension;

using Totten.Solution.RagnaComercio.WebApi.AppSettings;

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
        services.Configure<AppSettings>(configuration.GetSection("HttpClients"));

        return services;
    }
}
