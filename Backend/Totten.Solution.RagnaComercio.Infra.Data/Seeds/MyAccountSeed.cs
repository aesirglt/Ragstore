namespace Totten.Solution.RagnaComercio.Infra.Data.Seeds;

using Totten.Solution.RagnaComercio.Domain.Features.Accounts;

public class MyAccountSeed
{
    public static List<Account> Seed()
        => new()
        {
            new ()
            {
                Id = 1,
                Name = "account",
                IsReported = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        };
}
