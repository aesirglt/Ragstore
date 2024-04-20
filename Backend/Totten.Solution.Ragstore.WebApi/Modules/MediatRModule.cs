namespace Totten.Solution.Ragstore.WebApi.Modules;

using Autofac;
using MediatR;
using System.Reflection;
using Totten.Solution.Ragstore.ApplicationService;
using Totten.Solution.Ragstore.WebApi.Behaviors;
using Module = Autofac.Module;
/// <summary>
/// 
/// </summary>
public class MediatRModule : Module
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    protected override void Load(ContainerBuilder builder)
    {
        var assembly = typeof(ApplicationAssembly).GetTypeInfo().Assembly;

        builder.RegisterAssemblyTypes(assembly)
                   .AsClosedTypesOf(typeof(IRequestHandler<,>))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(assembly)
               .AsClosedTypesOf(typeof(IRequestHandler<>))
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(assembly)
               .AsClosedTypesOf(typeof(INotificationHandler<>))
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();

        builder.RegisterGeneric(typeof(LoggingBehavior<,>))
               .As(typeof(IPipelineBehavior<,>));

        builder.RegisterGeneric(typeof(ValidatorBehavior<,>))
               .As(typeof(IPipelineBehavior<,>));

        builder.RegisterGeneric(typeof(MultiTenantBehavior<,>))
               .As(typeof(IPipelineBehavior<,>));

    }
}