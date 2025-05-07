namespace Totten.Solution.RagnaComercio.Infra.Data.Seeds;

using Totten.Solution.RagnaComercio.Domain.Features.Accounts;

public class MyAccountSeed
{
    public static List<Account> Seed()
        => [
            new ()
            {
                Id = 1,
                Name = "account",
                IsReported = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new ()
            {
                Id = 2,
                Name = "account2",
                IsReported = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
        ];
}
