namespace Totten.Solution.Ragstore.WebApi.ServicesExtension;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Totten.Solution.Ragstore.Infra.Data.EntityFrameworkIdentity;
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
        services.AddAuthentication()
                .AddBearerToken(IdentityConstants.BearerScheme);
        services.AddAuthorizationBuilder();

        services
            .AddDbContext<AppIdentityContext>(x => x.UseSqlite("DataSource=app.db"))
            .AddIdentityCore<MyUserIdenty>()
            .AddEntityFrameworkStores<AppIdentityContext>()
            .AddApiEndpoints();

        return services;
    }
}
