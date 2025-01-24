namespace Totten.Solution.Ragstore.Infra.Data.Seeds;
using System;
using System.Collections.Generic;
using Totten.Solution.Ragstore.Domain.Features.Servers;

public static class MyServerSeed
{
    public static List<Server> Seed() => new()
    {
        new ()
        {
            Id = 1,
            CreatedAt = DateTime.UtcNow,
            Name = "broTHOR",
            SiteUrl = "https://playragnarokonlinebr.com",
            UpdatedAt = DateTime.UtcNow
        },
        new ()
        {
            Id = 2,
            CreatedAt = DateTime.UtcNow,
            Name = "broVALHALLA",
            SiteUrl = "https://playragnarokonlinebr.com",
            UpdatedAt = DateTime.UtcNow
        }
    };
}
