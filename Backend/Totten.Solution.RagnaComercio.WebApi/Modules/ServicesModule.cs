namespace Totten.Solution.RagnaComercio.WebApi.Modules;

using Autofac;
using Discord.WebSocket;
using Totten.Solution.RagnaComercio.ApplicationService.DTOs.Messages;
using Totten.Solution.RagnaComercio.ApplicationService.Interfaces;
using Totten.Solution.RagnaComercio.ApplicationService.Services;

/// <summary>
/// 
/// </summary>
public class ServicesModule : Autofac.Module
{
    IConfigurationRoot Configuration { get; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="configuration"></param>
    public ServicesModule(IConfigurationRoot configuration)
    {
        Configuration = configuration;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<WhatsAPPService>()
               .As<IMessageService<NotificationMessageDto>>()
               .InstancePerLifetimeScope();

        builder.RegisterType<DiscordSocketClient>()
               .AsSelf()
               .SingleInstance();

        builder.RegisterType<DiscordService>()
               .As<IMessageService<DiscordMessageDto>>()
               .SingleInstance();
    }
}