namespace Common.Tests.ObjectMothers;
using System;
using Totten.Solution.Ragstore.Domain.Features.Servers;

public static partial class ObjectMother
{
    public static Server Server => new()
    {
        Id = Guid.Parse("89300f33-33d7-4878-af08-1f7b694eca3f"),
        CreatedAt = DateTime.UtcNow,
        IsActive = true,
        Name = "latamro",
        DisplayName = "Latam RO",
        SiteUrl = "https://ro.gnjoylatam.com/",
        UpdatedAt = DateTime.UtcNow
    };
}
