﻿namespace Totten.Solution.RagnaComercio.WebApi.Modules;

using Autofac;
using FluentValidation;
using Totten.Solution.RagnaComercio.ApplicationService;

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