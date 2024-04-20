namespace Totten.Solution.Ragstore.Infra.Data.Seeds;
using Totten.Solution.Ragstore.Domain.Features.Characters;

public class MyCharacterSeed
{
    public static List<Character> Seed()
        => new()
        {
            new ()
            {
                Id = 1,
                Sex = 1,
                Name = "Mechanic 4i20",
                AccountId = 1,
                JobId = 1,
                BaseLevel = 99,
                Map = "Prontera",
                Location = "150,150",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            }
        };
}
