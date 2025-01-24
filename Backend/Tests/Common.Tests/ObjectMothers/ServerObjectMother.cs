namespace Common.Tests.ObjectMothers;
using System;
using Totten.Solution.Ragstore.Domain.Features.Servers;

public static partial class ObjectMother
{
    public static Server Server => new()
    {
        Id = 1,
        CreatedAt = DateTime.UtcNow,
        IsActive = true,
        Name = "bRO - thor",
        SiteUrl = "https://playragnarokonlinebr.com/",
        UpdatedAt = DateTime.UtcNow
    };
}
