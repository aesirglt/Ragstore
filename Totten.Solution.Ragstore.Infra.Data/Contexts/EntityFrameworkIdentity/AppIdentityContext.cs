namespace Totten.Solution.Ragstore.Infra.Data.Contexts.EntityFrameworkIdentity;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class AppIdentityContext : IdentityDbContext<MyUserIdenty>
{
    public AppIdentityContext(DbContextOptions<AppIdentityContext> options) : base(options)
    {

    }
}
