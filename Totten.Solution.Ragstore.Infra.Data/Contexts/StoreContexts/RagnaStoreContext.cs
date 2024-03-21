namespace Totten.Solution.Ragstore.Infra.Data.Contexts.StoreContexts;
using Microsoft.EntityFrameworkCore;

public class RagnaStoreContext : DbContext
{
    public RagnaStoreContext(DbContextOptions<RagnaStoreContext> options) : base(options)
    {

    }
}
