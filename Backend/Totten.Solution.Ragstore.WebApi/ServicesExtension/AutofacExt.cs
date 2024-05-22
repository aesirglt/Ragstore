namespace Totten.Solution.Ragstore.WebApi.ServicesExtension;

using Autofac;
using Autofac.Extensions.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection.Builder;
using Totten.Solution.Ragstore.WebApi.AppSettings;
using Totten.Solution.Ragstore.WebApi.Modules;

/// <summary>
/// 
/// </summary>
public static class AutofacExt
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="hostBuilder"></param>
    /// <param name="cfgRoot"></param>
    /// <returns></returns>
    public static IHostBuilder ConfigureAutofac(this IHostBuilder hostBuilder, IConfigurationRoot cfgRoot)
    {
        return hostBuilder
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>((context, containerBuilder) =>
            {
                var configuration = MediatRConfigurationBuilder
                                    .Create(typeof(Program).Assembly)
                                    .WithAllOpenGenericHandlerTypesRegistered()
                                    .WithRegistrationScope(RegistrationScope.Scoped)
                                    .Build();
                
                containerBuilder.RegisterModule(new FluentValidationModule());
                containerBuilder.RegisterModule(new GlobalModule<Program>(cfgRoot));
                containerBuilder.RegisterModule(new MediatRModule());
                containerBuilder.RegisterModule(new HttpClientsModule
                {
                    Settings = context.Configuration.Get<AppSettings>()
                });

                containerBuilder.RegisterMediatR(configuration);
                containerBuilder.Register(r => containerBuilder).AsSelf().InstancePerLifetimeScope();
            })
            .ConfigureHostOptions(o => o.ShutdownTimeout = TimeSpan.FromSeconds(60));
    }
}
