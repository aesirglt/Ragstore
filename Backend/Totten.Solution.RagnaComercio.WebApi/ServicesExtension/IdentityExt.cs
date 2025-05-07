namespace Totten.Solution.RagnaComercio.WebApi.ServicesExtension;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Totten.Solution.RagnaComercio.Infra.Data.Contexts.EntityFrameworkIdentity;
using Totten.Solution.RagnaComercio.WebApi.IdentityAggregation.Handlers;
using Totten.Solution.RagnaComercio.WebApi.IdentityAggregation.Requirements;

/// <summary>
/// 
/// </summary>
public static class IdentityExt
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IAuthorizationHandler, MinimumAgeHandler>();

        services.AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
                })
                .AddCookie(options =>
                {
                    options.LoginPath = "/auth/login"; // Página para login (caso o cookie expire ou o usuário não esteja autenticado)
                    options.LogoutPath = "/auth/logout"; // Página de logout
                    options.ExpireTimeSpan = TimeSpan.FromDays(1); // Tempo de expiração do cookie
                    options.SlidingExpiration = true; // O cookie será renovado automaticamente se o usuário continuar ativo
                })
                .AddGoogle(options =>
                {
                    options.ClientId = configuration["Authentication:Google:ClientId"];
                    options.ClientSecret = configuration["Authentication:Google:ClientSecret"];
                    options.CallbackPath = new PathString("/signin-google");
                })
                .AddOAuth("Discord", options =>
                {
                    options.ClientId = configuration["Authentication:Discord:ClientId"];
                    options.ClientSecret = configuration["Authentication:Discord:ClientSecret"];
                    options.AuthorizationEndpoint = "https://discord.com/api/oauth2/authorize";
                    options.TokenEndpoint = "https://discord.com/api/oauth2/token";
                    options.UserInformationEndpoint = "https://discord.com/api/users/@me";
                    options.CallbackPath = new PathString("/signin-discord");
                    options.Scope.Add("identify");
                    options.Scope.Add("email");
                });

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
