namespace Totten.Solution.RagnaComercio.Infra.Data.Seeds;
using Totten.Solution.RagnaComercio.Domain.Features.Characters;

public class MyCharacterSeed
{
    public const string CharName = "Mechanic 4i20";
    public const string CharName2 = "Mercador 4i20";
    public static List<Character> Seed()
        => [
            new ()
            {
                Id = 1,
                Sex = 1,
                Name = CharName,
                AccountId = 1,
                JobId = 1,
                BaseLevel = 99,
                Map = "Prontera",
                Location = "150,150",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new ()
            {
                Id = 2,
                Sex = 1,
                Name = CharName2,
                AccountId = 2,
                JobId = 1,
                BaseLevel = 99,
                Map = "Prontera",
                Location = "150,150",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
        ];
}
