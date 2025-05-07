namespace Totten.Solution.RagnaComercio.Infra.Data.Seeds;
using System;
using System.Collections.Generic;
using Totten.Solution.RagnaComercio.Domain.Features.Servers;

public static class MyServerSeed
{
    public static Guid Id => Guid.Parse("89300f33-33d7-4878-af08-1f7b694eca3f");

    public static List<Server> Seed()
    => [
        new ()
        {
            Id = Guid.Parse("89300f33-33d7-4878-af08-1f7b694eca3f"),
            CreatedAt = DateTime.UtcNow,
            Name = "latamro",
            DisplayName = "Latam RO",
            SiteUrl = "https://ro.gnjoylatam.com/",
            UpdatedAt = DateTime.UtcNow
        }
    ];
}
