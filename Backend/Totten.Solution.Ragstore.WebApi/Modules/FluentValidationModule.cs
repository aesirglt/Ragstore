namespace Totten.Solution.Ragstore.WebApi.Modules;

using Autofac;
using FluentValidation;
using Totten.Solution.Ragstore.ApplicationService;

/// <summary>
/// 
/// </summary>
public class FluentValidationModule : Module
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(typeof(ApplicationAssembly).Assembly)
            .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
    }
}