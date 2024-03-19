namespace Totten.Solution.Ragstore.WebApi.ServicesExtension;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Totten.Solution.Ragstore.Infra.Data.EntityFrameworkIdentity;
using Totten.Solution.Ragstore.WebApi.IdentityAgreggation.Handlers;
using Totten.Solution.Ragstore.WebApi.IdentityAgreggation.Requirements;

/// <summary>
/// 
/// </summary>
public static class IdentityExt
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
    {
        services.AddSingleton<IAuthorizationHandler, MinimumAgeHandler>();

        services.AddAuthentication()
                .AddBearerToken(IdentityConstants.BearerScheme);
        services.AddAuthorizationBuilder()
                .AddPolicy("AgePolicy", policy =>
                                        policy.Requirements.Add(new MinimumAgeRequirement(21)));

        services
            .AddDbContext<AppIdentityContext>(x => x.UseSqlite("DataSource=app.db"))
            .AddIdentityCore<MyUserIdenty>()
            .AddEntityFrameworkStores<AppIdentityContext>()
            .AddApiEndpoints();
        return services;
    }
}
