namespace Totten.Solution.Ragstore.WebApi.Modules;

using Autofac;
using Totten.Solution.Ragstore.ApplicationService.Features.ItemsAggregation.QueriesHandler;
using Totten.Solution.Ragstore.WebApi.AppSettings;

/// <summary>
/// 
/// </summary>
public class HttpClientsModule : Module
{
    /// <summary>
    /// 
    /// </summary>
    public required AppSettings Settings { get; init; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    protected override void Load(ContainerBuilder builder)
    {
        builder.Register(c =>
        {
            var httpClientFactory = c.Resolve<IHttpClientFactory>();

            return httpClientFactory.CreateClient();
        });

        foreach (var item in Settings.HttpClients)
        {
            builder.Register(c =>
            {
                var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(item.Url);
                foreach (var header in item.DefaultHeaders)
                {
                    httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                }

                return httpClient;
            }).Named(item.Name, typeof(HttpClient))
            .InstancePerLifetimeScope();

            builder.Register(c => item.DefaultInQueryData).Named(item.Name, typeof(Dictionary<string, string>))
                   .InstancePerLifetimeScope();
        }
    }
}