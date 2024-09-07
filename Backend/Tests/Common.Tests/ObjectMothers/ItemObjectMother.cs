namespace Common.Tests.ObjectMothers;

using Totten.Solution.Ragstore.Domain.Features.ItemsAggregation;

public static partial class ObjectMother
{
    public static Item ItemRedPotion => new()
    {
        Id = 501,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow,
        Name = "Poção Vermelha",
        Type = null,
        Description = "",
        Slots = null,
        SubType = null
    };

    public static Item ItemAnelObscuro => new()
    {
        Id = 490037,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow,
        Name = "Anel Obscuro",
        Type = null,
        Description = "",
        Slots = null,
        SubType = null
    };
}
